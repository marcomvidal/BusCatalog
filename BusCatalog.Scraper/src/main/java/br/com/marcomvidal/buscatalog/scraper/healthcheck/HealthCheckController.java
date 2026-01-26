package br.com.marcomvidal.buscatalog.scraper.healthcheck;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import br.com.marcomvidal.buscatalog.scraper.healthcheck.ports.HealthCheckResponse;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@RestController
@RequestMapping("/api/health-check")
@Tag(name = "HealthCheck")
public class HealthCheckController {
    private final HealthCheckService service;

    public HealthCheckController(HealthCheckService service) {
        this.service = service;
    }

    @Operation(summary = "Responds a health check request.")
    @ApiResponse(responseCode = "200", description = "Application is up & running")
    @GetMapping
    public ResponseEntity<HealthCheckResponse> get() {
        return service.selfHealthCheck().toResult();
    }

    @Operation(summary = "Sends a HTTP request against BusCatalog.Api.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Application is up & running"),
        @ApiResponse(responseCode = "503", description = "Application is unreachable")
    })
    @GetMapping("/api")
    public ResponseEntity<HealthCheckResponse> api() {
        return service.apiHealthCheck().toResult();
    }
}
