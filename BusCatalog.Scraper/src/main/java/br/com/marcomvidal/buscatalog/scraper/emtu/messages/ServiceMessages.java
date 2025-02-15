package br.com.marcomvidal.buscatalog.scraper.emtu.messages;

public class ServiceMessages {
    public static final String NETWORK_FAILURE_LABEL = "NetworkFailure";
    public static final String NETWORK_FAILURE = "Obtaining line {} data from EMTU website failed due a network error.";
    public static final String LINE_NOT_FOUND_LABEL = "LineNotFound";
    public static final String LINE_NOT_FOUND = "The specified line {} does not exists in the EMTU website.";
    public static final String LINE_DATA_FETCHED_SUCCESSFULLY = "Obtained line {} data from EMTU website successfully.";
    public static final String LINE_DATA_SCRAPED_SUCCESSFULLY = "Scraped line {} data from EMTU website response successfully.";
    public static final String LINE_DENOMINATION_FETCHED_SUCCESSFULLY = "Obtained line {} denomination from EMTU website successfully.";
    public static final String LINE_DENOMINATION_SCRAPED_SUCCESSFULLY = "Scraped line {} denomination from EMTU website response successfully.";
}
