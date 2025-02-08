package br.com.marcomvidal.buscatalog.scraper.synchronization;

import java.util.List;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.api.adapters.BusCatalogApiHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SyncType;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SynchronizedLine;

@Service
public class SynchronizationService {
    private final SynchronizationRepository synchronizationRepository;
    private final SynchronizedLineRepository synchronizedLineRepository;
    private final BusCatalogApiHttpAdapter apiHttpAdapter;

    public SynchronizationService(
        SynchronizationRepository synchronizationRepository,
        SynchronizedLineRepository synchronizedLineRepository,
        BusCatalogApiHttpAdapter apiHttpAdapter) {
            this.synchronizationRepository = synchronizationRepository;
            this.synchronizedLineRepository = synchronizedLineRepository;
            this.apiHttpAdapter = apiHttpAdapter;
    }

    public List<Synchronization> getAll() {
        return synchronizationRepository.findAll();
    }

    public void persist(List<Line> lines) {
        var synchronization = new Synchronization(SyncType.REST_API);
        synchronizationRepository.save(synchronization);

        for (var line : lines) {
            // var response = apiHttpAdapter.save(line);
            var synchronizedLine = new SynchronizedLine(line.getIdentification(), synchronization);
            synchronizedLineRepository.save(synchronizedLine);
        }
    }
}
