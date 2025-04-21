package br.com.marcomvidal.buscatalog.scraper.emtu.services;

import java.io.IOException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.emtu.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.emtu.scrapers.EmtuLineDenominationScraper;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class EmtuLineDenominationService {
    private final EmtuHttpAdapter adapter;
    private final Logger logger = LoggerFactory.getLogger(EmtuLineDenominationService.class);

    public EmtuLineDenominationService(EmtuHttpAdapter adapter) {
        this.adapter = adapter;
    }

    public LineServiceResponse<String> query(String identifier) {
        try {
            var response = adapter.getDenomination(identifier);
            var denomination = new EmtuLineDenominationScraper(response).scrap();
            logger.info(ServiceMessages.LINE_DENOMINATION_SCRAPED_SUCCESSFULLY.getMessage(), identifier);

            return new LineServiceResponse<String>(denomination);
        } catch (IOException ex) {
            logger.warn(ServiceMessages.NETWORK_FAILURE.getMessage(), identifier);

            return new LineServiceResponse<String>(
                identifier,
                ServiceMessages.NETWORK_FAILURE.name());
        } catch (Exception ex) {
            logger.warn(ServiceMessages.LINE_NOT_FOUND.getMessage(), identifier);
            
            return new LineServiceResponse<String>(
                identifier,
                ServiceMessages.LINE_NOT_FOUND.name());
        }
    }
}
