package br.com.marcomvidal.buscatalog.scraper.synchronization.messages;

public class ServiceMessages {
    public static final String FETCHED_ALL_SYNCS = "Fetched all line synchronizations: {}.";
    public static final String SAVED_SYNC_DATA_AT_API = "Saved line {} synchronization data at BusCatalog.Api.";
    public static final String SAVED_SYNC_DATA_AT_API_FAILURE = "BusCatalog.Api responded with errors: Line: {}, HTTP Status: {}, Message: {}.";
    public static final String SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE = "Line synchronization failed unexpectedly: Line: {}, Message: {}.";
    public static final String SAVED_SYNC_AT_SCRAPER = "Saved line {} synchronization at BusCatalog.Scraper database.";
}
