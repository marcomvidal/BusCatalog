package br.com.marcomvidal.buscatalog.scraper.healthcheck.e2e;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.reactive.server.WebTestClient;

import static org.assertj.core.api.Assertions.assertThat;

import br.com.marcomvidal.buscatalog.scraper.healthcheck.ports.HealthCheckResponse;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class SelfHealthCheckTest {
    private final String URL = "/api/health-check";

    @Autowired
    private WebTestClient webClient;

    @Test
    public void whenGetRequestIsSent_shouldRespondSuccessfully() {
        webClient.get()
            .uri(URL)
            .exchange()
            .expectStatus()
            .isOk();
    }

    @Test
    public void whenGetRequestIsSent_bodyShouldIndicateSuccess() {
        var result = webClient.get()
            .uri(URL)
            .exchange()
            .expectStatus()
            .isOk()
            .expectBody(HealthCheckResponse.class)
            .returnResult()
            .getResponseBody();

        assertThat(result).satisfies(x -> {
            assertThat(x.getUrl()).contains(URL);
            assertThat(x.isSuccess()).isTrue();
        });
    }
}
