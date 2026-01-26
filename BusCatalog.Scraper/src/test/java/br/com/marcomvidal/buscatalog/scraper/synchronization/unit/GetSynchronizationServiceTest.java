package br.com.marcomvidal.buscatalog.scraper.synchronization.unit;

import static org.assertj.core.api.Assertions.assertThat;

import java.util.ArrayList;
import java.util.List;

import org.junit.jupiter.api.Test;
import org.mockito.Mockito;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SyncType;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;
import br.com.marcomvidal.buscatalog.scraper.synchronization.services.GetSynchronizationService;

public class GetSynchronizationServiceTest {
    private SynchronizationRepository repository;

    public GetSynchronizationServiceTest() {
        this.repository = Mockito.mock(SynchronizationRepository.class);
    }

    @Test
    public void whenThereAreSynchronizations_shoulReturnThem() {
        Mockito.when(repository.findAll())
            .thenReturn(
                List.of(
                    new Synchronization(SyncType.REST_API),
                    new Synchronization(SyncType.KAFKA_TOPIC)));

        var service = new GetSynchronizationService(repository);

        assertThat(service.getAll()).hasSize(2);
    }

    @Test
    public void whenThereAreNoSynchronizations_shoudlReturnAnEmptyList() {
        Mockito.when(repository.findAll())
            .thenReturn(new ArrayList<Synchronization>());

        var service = new GetSynchronizationService(repository);

        assertThat(service.getAll()).isEmpty();
    }
}
