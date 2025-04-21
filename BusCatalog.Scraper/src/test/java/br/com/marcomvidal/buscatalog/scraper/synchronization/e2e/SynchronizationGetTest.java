package br.com.marcomvidal.buscatalog.scraper.synchronization.e2e;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.reactive.server.WebTestClient;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class SynchronizationGetTest {
    private final String URL = "/api/synchronizations";

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
    public void whenGetRequestIsSent_shouldContainSynchronizations() {
        webClient.get()
            .uri(URL)
            .exchange()
            .expectStatus()
            .isOk()
            .expectBodyList(Synchronization.class);
    }
}
