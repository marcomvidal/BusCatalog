package br.com.marcomvidal.buscatalog.scraper.healthcheck.messages;

public enum ServiceMessages {
    HEALTH_CHECK_RESULT("Sent HealthCheck to BusCatalog.{}: URL {}, StatusCode: {}");

    private final String message;

    ServiceMessages(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }
}