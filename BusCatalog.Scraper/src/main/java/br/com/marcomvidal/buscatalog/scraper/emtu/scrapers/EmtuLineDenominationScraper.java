package br.com.marcomvidal.buscatalog.scraper.emtu.scrapers;

import org.jsoup.nodes.Document;

public class EmtuLineDenominationScraper {
    private static final String IDENTIFICATION_XPATH = "/html/body/div[1]/div/div/span/form/span/table/tbody/tr[1]/td[1]/a";
    private static final String IDENTIFICATION_QUERY_STRING = "numlinha=";
    private final Document document;

    public EmtuLineDenominationScraper(Document document) {
        this.document = document;
    }

    public String scrap() {
        var link = document
            .selectXpath(IDENTIFICATION_XPATH)
            .toString();

        return link.substring(
            link.indexOf(IDENTIFICATION_QUERY_STRING) + IDENTIFICATION_QUERY_STRING.length(),
            link.indexOf("')"));
    }
}
