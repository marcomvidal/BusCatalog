package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineSyncRequest;
import br.com.marcomvidal.buscatalog.scraper.line.services.LineProducerService;
import br.com.marcomvidal.buscatalog.scraper.line.services.RestLineSyncService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@RestController
@RequestMapping("/api/lines")
@Tag(name = "Line")
public class LinesController {
    private final LineService lineService;
    private final RestLineSyncService restLineSyncService;
    private final LineProducerService lineProducerService;

    public LinesController(
        LineService lineService,
        RestLineSyncService restLineSyncService,
        LineProducerService lineProducerService) {
        this.lineService = lineService;
        this.restLineSyncService = restLineSyncService;
        this.lineProducerService = lineProducerService;
    }

    @Operation(summary = "Gets data from the official EMTU source for a list of lines.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved data"),
        @ApiResponse(responseCode = "400", description = "Invalid line list")
    })
    @GetMapping
    public ResponseEntity<LineResponse> get(@RequestParam List<String> lines) {
        var response = lineService.query(lines);

        return response.hasLines()
            ? ResponseEntity.ok(response)
            : ResponseEntity.badRequest().body(response);
    }

    @Operation(summary = "Gets list of lines from EMTU and synchronize them via REST into BusCatalog.Api.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully synchronized data"),
        @ApiResponse(responseCode = "400", description = "Invalid line list")
    })
    @PostMapping("/rest")
    public ResponseEntity<LineResponse> rest(@RequestBody LineSyncRequest request) {
        var response = lineService.query(request.getLines());

        if (!response.hasLines()) {
            return ResponseEntity.badRequest().body(response);
        }

        var errors = restLineSyncService.sync(response.getLines());

        return errors.isEmpty()
            ? ResponseEntity.ok(response)
            : ResponseEntity.unprocessableEntity().body(response.withErrors(errors));
    }

    @Operation(summary = "Gets list of lines from EMTU and produce them to Kafka topic.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully synchronized data"),
        @ApiResponse(responseCode = "400", description = "Invalid line list")
    })
    @PostMapping("/producer")
    public ResponseEntity<LineResponse> producer(@RequestBody LineSyncRequest request) {
        var response = lineService.query(request.getLines());

        if (!response.hasLines()) {
            return ResponseEntity.badRequest().body(response);
        }

        var errors = lineProducerService.sync(response.getLines());

        return errors.isEmpty()
            ? ResponseEntity.ok(response)
            : ResponseEntity.unprocessableEntity().body(response.withErrors(errors));
    }
}
