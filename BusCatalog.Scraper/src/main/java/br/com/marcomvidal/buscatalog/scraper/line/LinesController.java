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
import br.com.marcomvidal.buscatalog.scraper.synchronization.SynchronizationService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@RestController
@RequestMapping("/api/lines")
@Tag(name = "Line")
public class LinesController {
    private final LineService lineService;
    private final SynchronizationService synchronizationService;

    public LinesController(
        LineService lineService,
        SynchronizationService synchronizationService) {
        this.lineService = lineService;
        this.synchronizationService = synchronizationService;
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
    @PostMapping("/sync")
    public ResponseEntity<LineResponse> post(@RequestBody LineSyncRequest request) {
        var response = lineService.query(request.getLines());

        if (!response.hasLines()) {
            return ResponseEntity.badRequest().body(response);
        }

        var errors = synchronizationService.persist(response.getLines());

        return errors.isEmpty()
            ? ResponseEntity.ok(response)
            : ResponseEntity.unprocessableEntity().body(response.withErrors(errors));
    }
}
