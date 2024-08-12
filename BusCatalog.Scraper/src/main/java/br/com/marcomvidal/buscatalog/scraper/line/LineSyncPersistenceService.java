package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.api.adapters.BusCatalogApiHttpAdapter;

@Service
public class LineSyncPersistenceService {
    private final BusCatalogApiHttpAdapter apiHttpAdapter;

    public LineSyncPersistenceService(BusCatalogApiHttpAdapter apiHttpAdapter) {
        this.apiHttpAdapter = apiHttpAdapter;
    }

    public void persist(List<Line> lines) {
        for (var line : lines) {
            var response = apiHttpAdapter.save(line);
        }
    }
}
