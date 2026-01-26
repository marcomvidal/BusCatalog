package br.com.marcomvidal.buscatalog.scraper.synchronization.messages;

public enum ServiceMessages {
    SAVED_SYNC_DATA_AT_API("Saved line {} synchronization data at BusCatalog.Api."),
    SAVED_SYNC_DATA_AT_API_FAILURE("BusCatalog.Api responded with errors: Message: {}."),
    SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE("Line synchronization failed unexpectedly: Message: {}."),
    FETCHED_ALL_SYNCS("Fetched all line synchronizations: {}."),
    SAVED_SYNC_AT_SCRAPER("Saved line {} synchronization at BusCatalog.Scraper database.");

    private final String message;

    ServiceMessages(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }
}