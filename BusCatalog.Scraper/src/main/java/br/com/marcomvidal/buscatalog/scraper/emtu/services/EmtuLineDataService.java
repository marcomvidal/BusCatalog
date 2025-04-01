package br.com.marcomvidal.buscatalog.scraper.emtu.services;

import java.io.IOException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuVehicleFactory;
import br.com.marcomvidal.buscatalog.scraper.emtu.adapters.EmtuHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.emtu.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.emtu.scrapers.EmtuLineDataScraper;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class EmtuLineDataService {
    private final EmtuHttpAdapter adapter;
    private Logger logger = LoggerFactory.getLogger(EmtuLineDataService.class);

    public EmtuLineDataService(EmtuHttpAdapter adapter) {
        this.adapter = adapter;
    }

    public LineServiceResponse<Line> get(String identifier) {
        try {
            var response = adapter.getData(identifier);
            
            var line = new EmtuLineDataScraper(response)
                .scrap()
                .withVehicles(EmtuVehicleFactory.generate(identifier));
            
            logger.info(
                ServiceMessages.LINE_DATA_SCRAPED_SUCCESSFULLY.getMessage(),
                identifier);

            return new LineServiceResponse<Line>(line);
        } catch (IOException ex) {
            logger.warn(ServiceMessages.NETWORK_FAILURE.getMessage(), identifier);
            
            return new LineServiceResponse<Line>(
                identifier,
                ServiceMessages.NETWORK_FAILURE.name());
        } catch (Exception ex) {
            logger.warn(ServiceMessages.LINE_NOT_FOUND.getMessage(), identifier);
            
            return new LineServiceResponse<Line>(
                identifier,
                ServiceMessages.LINE_NOT_FOUND.name());
        }
    }
}
