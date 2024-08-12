package br.com.marcomvidal.buscatalog.scraper.emtu.denomination;

import java.io.IOException;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.adapters.EmtuHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class EmtuLineDenominationService {
    private final EmtuHttpAdapter adapter;

    public EmtuLineDenominationService(EmtuHttpAdapter adapter) {
        this.adapter = adapter;
    }

    public LineServiceResponse<String> query(String identifier) {
        try {
            var response = adapter.getDenomination(identifier);
            var denomination = new EmtuLineDenominationScraper(response).scrap();

            return new LineServiceResponse<String>(denomination);
        } catch (IOException ex) {
            return new LineServiceResponse<String>(identifier, "NetworkFailure");
        } catch (Exception ex) {
            return new LineServiceResponse<String>(identifier, "LineNotFound");
        }
    }
}
