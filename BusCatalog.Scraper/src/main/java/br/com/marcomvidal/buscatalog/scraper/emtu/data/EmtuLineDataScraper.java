package br.com.marcomvidal.buscatalog.scraper.emtu.data;

import org.jsoup.nodes.Document;

import br.com.marcomvidal.buscatalog.scraper.line.Line;

public class EmtuLineDataScraper {
    private static final String ITINERARY_XPATH = "/html/body/div/div/div/table/tbody/tr/td/table/tbody/tr/td/div/table[1]/tbody/tr[@row]/td";
    private static final String IDENTIFICATION_XPATH = "/html/body/div/div/div/table/tbody/tr/td/table/tbody/tr/td/div/table[1]/tbody/tr[1]/td[1]";
    private static final String DEPARTURES_XPATH = "/html/body/div/div/div/table/tbody/tr/td/table/tbody/tr/td/div/table[2]/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[2]/td";
    private final Document document;

    public EmtuLineDataScraper(Document document) {
        this.document = document;
    }

    public Line scrap() {
        return new Line(
            getIdentification(),
            getItinerary("Terminal Inicial :", "4"),
            getItinerary("Terminal Final :", "5"),
            getDeparturesPerDay());
    }

    private String getIdentification() {
        return document
            .selectXpath(IDENTIFICATION_XPATH)
            .first()
            .text()
            .replace("Linha:", "")
            .trim();
    }

    private String getItinerary(String rowLabel, String rowNumber) {
        return document
            .selectXpath(ITINERARY_XPATH.replace("@row", rowNumber))
            .first()
            .text()
            .replace(rowLabel, "")
            .trim();
    }

    private int getDeparturesPerDay() {
        var section = document
            .selectXpath(DEPARTURES_XPATH)
            .first()
            .text();

        return section.substring(
            section.indexOf("Dias Úteis") + "Dias Úteis".length(),
            section.indexOf("Sábados"))
            .trim()
            .split(" ")
            .length;
    }
}
