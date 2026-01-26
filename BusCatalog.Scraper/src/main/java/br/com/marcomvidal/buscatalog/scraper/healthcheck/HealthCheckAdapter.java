package br.com.marcomvidal.buscatalog.scraper.healthcheck;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

import br.com.marcomvidal.buscatalog.scraper.healthcheck.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.healthcheck.ports.HealthCheckResponse;

@Service
public class HealthCheckAdapter {
    private final RestClient http;
    private final Logger logger = LoggerFactory.getLogger(HealthCheckAdapter.class);
    private final int INVALID_STATUS_CODE = HttpStatus.SERVICE_UNAVAILABLE.value();

    public HealthCheckAdapter(RestClient.Builder http) {
        this.http = http.build();
    }

    public HealthCheckResponse send(String url, String application) {
        try {
            return handleSuccess(url, application);
        } catch (Exception ex) {
            return handleError(url, application);
        }
    }

    private HealthCheckResponse handleSuccess(String url, String application) {
        var response = http.get().uri(url).retrieve().toBodilessEntity();
            
        var statusCode = response.getStatusCode();

        logger.info(
            ServiceMessages.HEALTH_CHECK_RESULT.getMessage(),
            application,
            url,
            statusCode);

        return HealthCheckResponse.generate(
            url,
            statusCode.value(),
            statusCode.is2xxSuccessful());
    }

    private HealthCheckResponse handleError(String url, String application) {
        logger.warn(
            ServiceMessages.HEALTH_CHECK_RESULT.getMessage(),
            application,
            url,
            INVALID_STATUS_CODE);

        return HealthCheckResponse.generate(url, INVALID_STATUS_CODE, false);
    }
}
