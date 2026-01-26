package br.com.marcomvidal.buscatalog.scraper.emtu.messages;

public enum ServiceMessages {
    NETWORK_FAILURE("Obtaining line {} data from EMTU website failed due a network error."),
    LINE_NOT_FOUND("The specified line {} does not exists in the EMTU website."),
    LINE_DATA_FETCHED_SUCCESSFULLY("Obtained line {} data from EMTU website successfully."),
    LINE_DATA_SCRAPED_SUCCESSFULLY("Scraped line {} data from EMTU website response successfully."),
    LINE_DENOMINATION_FETCHED_SUCCESSFULLY("Obtained line {} denomination from EMTU website successfully."),
    LINE_DENOMINATION_SCRAPED_SUCCESSFULLY("Scraped line {} denomination from EMTU website response successfully.");

    private final String message;

    ServiceMessages(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }
}
