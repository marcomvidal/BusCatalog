package br.com.marcomvidal.buscatalog.scraper.api.adapters;

import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

import br.com.marcomvidal.buscatalog.scraper.api.ports.SaveLineResponse;
import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.Configuration;
import br.com.marcomvidal.buscatalog.scraper.line.Line;

@Service
public class BusCatalogApiHttpAdapter {
    private final RestClient http;
    private final String SAVE_LINE_URL = "/api/lines";

    public BusCatalogApiHttpAdapter(
        Configuration configuration,
        RestClient.Builder httpBuilder) {
        var baseUrl = configuration.busCatalog.url;
        this.http = httpBuilder.baseUrl(baseUrl).build();
    }

    public ResponseEntity<SaveLineResponse> save(Line line) {
        return http.post()
            .uri(SAVE_LINE_URL)
            .body(line)
            .retrieve()
            .toEntity(SaveLineResponse.class);
    }
}
