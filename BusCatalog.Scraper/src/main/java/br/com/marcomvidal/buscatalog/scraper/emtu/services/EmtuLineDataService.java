package br.com.marcomvidal.buscatalog.scraper.emtu.services;

import java.io.IOException;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuVehicleFactory;
import br.com.marcomvidal.buscatalog.scraper.emtu.adapters.EmtuHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.emtu.scrapers.EmtuLineDataScraper;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class EmtuLineDataService {
    private final EmtuHttpAdapter adapter;

    public EmtuLineDataService(EmtuHttpAdapter adapter) {
        this.adapter = adapter;
    }

    public LineServiceResponse<Line> get(String identifier) {
        try {
            var response = adapter.getData(identifier);
            var line = new EmtuLineDataScraper(response)
                .scrap()
                .withVehicles(EmtuVehicleFactory.generate(identifier));

            return new LineServiceResponse<Line>(line);
        } catch (IOException ex) {
            return new LineServiceResponse<Line>(identifier, "NetworkFailure");
        } catch (Exception ex) {
            return new LineServiceResponse<Line>(identifier, "LineNotFound");
        }
    }
}
