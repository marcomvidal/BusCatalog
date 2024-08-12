package br.com.marcomvidal.buscatalog.scraper.api.adapters;

import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.Configuration;
import br.com.marcomvidal.buscatalog.scraper.line.Line;

@Service
public class BusCatalogApiHttpAdapter {
    private final RestClient http;

    public BusCatalogApiHttpAdapter(
        Configuration configuration,
        RestClient.Builder httpBuilder) {
        var baseUrl = configuration.busCatalog.url;
        this.http = httpBuilder.baseUrl(baseUrl).build();
    }

    public ResponseEntity<String> save(Line line) {
        return http.post()
            .uri("/api/lines")
            .body(line)
            .retrieve()
            .toEntity(String.class);
    }
}
