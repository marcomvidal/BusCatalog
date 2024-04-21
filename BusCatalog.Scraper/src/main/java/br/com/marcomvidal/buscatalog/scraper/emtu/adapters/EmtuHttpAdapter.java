package br.com.marcomvidal.buscatalog.scraper.emtu.adapters;

import java.io.IOException;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuConfiguration;

@Service
public class EmtuHttpAdapter {
    private final String identificationUrl;
    private final String dataUrl;

    public EmtuHttpAdapter(EmtuConfiguration configuration) {
        this.identificationUrl = configuration.identificationUrl;
        this.dataUrl = configuration.dataUrl;
    }

    public Document getDenomination(String identifier) throws IOException {
        var url = identificationUrl.replace("@id", identifier);
        
        return Jsoup.connect(url).get();
    }
    
    public Document getData(String denomination) throws IOException {
        var url = dataUrl + denomination;

        return Jsoup.connect(url).get();
    }
}
