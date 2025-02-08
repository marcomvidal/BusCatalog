package br.com.marcomvidal.buscatalog.scraper.synchronization;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@RestController
@RequestMapping("/api/synchronization")
@Tag(name = "Synchronization")
public class SynchonizationController {
    private final SynchronizationService synchronizationService;

    public SynchonizationController(SynchronizationService synchronizationService) {
        this.synchronizationService = synchronizationService;
    }

    @Operation(summary = "Gets all synchronizations made.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved data")
    })
    @GetMapping
    public ResponseEntity<List<Synchronization>> get() {
        var synchronizations = synchronizationService.getAll();

        return ResponseEntity.ok(synchronizations);
    }
}
