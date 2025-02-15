package br.com.marcomvidal.buscatalog.scraper.emtu.adapters;

import java.io.IOException;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.Configuration;

@Service
public class EmtuHttpAdapter {
    private final String identificationUrl;
    private final String dataUrl;

    public EmtuHttpAdapter(Configuration configuration) {
        this.identificationUrl = configuration.emtu.identificationUrl;
        this.dataUrl = configuration.emtu.dataUrl;
    }

    public Document getDenomination(String identifier) throws IOException {
        return Jsoup
            .connect(identificationUrl.replace("@id", identifier))
            .get();
    }
    
    public Document getData(String denomination) throws IOException {
        return Jsoup
            .connect(dataUrl + denomination)
            .get();
    }
}
