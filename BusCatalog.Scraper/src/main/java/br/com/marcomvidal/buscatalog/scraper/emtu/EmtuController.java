package br.com.marcomvidal.buscatalog.scraper.emtu;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import br.com.marcomvidal.buscatalog.scraper.line.LineQueryService;
import br.com.marcomvidal.buscatalog.scraper.line.LineSyncPersistenceService;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@RestController
@RequestMapping("/api/emtu")
@Tag(name = "Emtu")
public class EmtuController {
    private final LineQueryService queryService;
    private final LineSyncPersistenceService syncPersistenceService;

    public EmtuController(
        LineQueryService queryService,
        LineSyncPersistenceService syncPersistenceService) {
        this.queryService = queryService;
        this.syncPersistenceService = syncPersistenceService;
    }

    @Operation(summary = "Gets data from the official EMTU source for a list of lines.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved data"),
        @ApiResponse(responseCode = "400", description = "Invalid line list")
    })
    @GetMapping
    public ResponseEntity<LineResponse> get(@RequestParam List<String> lines) {
        var response = queryService.query(lines);

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
    public ResponseEntity<LineResponse> post(@RequestBody List<String> lines) {
        var response = queryService.query(lines);

        if (!response.hasLines()) {
            return ResponseEntity.badRequest().body(response);
        }

        syncPersistenceService.persist(response.getLines());

        return ResponseEntity.ok(response);
    }
}
