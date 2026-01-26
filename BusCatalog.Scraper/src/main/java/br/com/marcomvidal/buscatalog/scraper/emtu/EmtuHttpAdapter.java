package br.com.marcomvidal.buscatalog.scraper.emtu;

import java.io.IOException;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;

@Service
public class EmtuHttpAdapter {
    private final String identificationUrl;
    private final String dataUrl;

    public EmtuHttpAdapter(ConfigurationSection configuration) {
        this.identificationUrl = configuration.getEmtu().identificationUrl;
        this.dataUrl = configuration.getEmtu().dataUrl;
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
