package br.com.marcomvidal.buscatalog.scraper.line.adapters.buscatalogapi;

import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.line.adapters.buscatalogapi.ports.SaveLineResponse;

@Service
public class ApiLinesAdapter {
    private final RestClient http;
    private final String SAVE_LINE_URL = "/api/lines";

    public ApiLinesAdapter(
        ConfigurationSection configuration,
        RestClient.Builder httpBuilder) {
        this.http = httpBuilder
            .baseUrl(configuration.apiUrl)
            .build();
    }

    public ResponseEntity<SaveLineResponse> save(Line line) {
        return http.post()
            .uri(SAVE_LINE_URL)
            .body(line)
            .retrieve()
            .toEntity(SaveLineResponse.class);
    }
}
