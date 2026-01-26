package br.com.marcomvidal.buscatalog.scraper.healthcheck.ports;

import java.time.LocalDateTime;

import org.springframework.http.ResponseEntity;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@AllArgsConstructor
@Getter
@Setter
public class HealthCheckResponse {
    private String url;
    private LocalDateTime time;
    private int statusCode;
    private boolean success;

    public static HealthCheckResponse generate(String url, int statusCode, boolean success) {
        return new HealthCheckResponse(
            url, 
            LocalDateTime.now(), 
            statusCode, 
            success);
    }

    public ResponseEntity<HealthCheckResponse> toResult() {
        return ResponseEntity
            .status(this.statusCode)
            .body(this);
    }
}
