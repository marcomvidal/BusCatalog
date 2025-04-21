package br.com.marcomvidal.buscatalog.scraper.healthcheck;

import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.healthcheck.ports.HealthCheckResponse;
import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;
import jakarta.servlet.http.HttpServletRequest;

@Service
public class HealthCheckService {
    private final HealthCheckAdapter adapter;
    private HttpServletRequest request;
    private final ConfigurationSection configuration;
    private final String API_HEALTH_CHECK_URL = "/api/health-check";

    public HealthCheckService(
        HealthCheckAdapter adapter,
        HttpServletRequest request,
        ConfigurationSection configuration) {
        this.adapter = adapter;
        this.request = request;
        this.configuration = configuration;
    }

    public HealthCheckResponse selfHealthCheck() {
        return HealthCheckResponse.generate(
            request.getRequestURL().toString(),
            HttpStatus.OK.value(),
            true);
    }

    public HealthCheckResponse apiHealthCheck() {
        return adapter.send(
            configuration.getApiUrl() + API_HEALTH_CHECK_URL,
            HealthCheckApplication.API.name());
    }
}
