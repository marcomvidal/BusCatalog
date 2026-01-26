package br.com.marcomvidal.buscatalog.scraper.line.messages;

public enum ServiceMessages {
    LINE_QUERY_FAILED("Line {} querying had errors."),
    LINE_QUERY_CONCLUDED("Line {} querying has concluded.");

    private final String message;

    ServiceMessages(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }
}