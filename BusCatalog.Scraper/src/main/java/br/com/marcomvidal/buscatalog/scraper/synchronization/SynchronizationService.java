package br.com.marcomvidal.buscatalog.scraper.synchronization;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.web.client.HttpServerErrorException;

import br.com.marcomvidal.buscatalog.scraper.api.adapters.BusCatalogApiHttpAdapter;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SyncType;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SynchronizedLine;
import br.com.marcomvidal.buscatalog.scraper.synchronization.messages.ServiceMessages;

@Service
public class SynchronizationService {
    private final SynchronizationRepository synchronizationRepository;
    private final SynchronizedLineRepository synchronizedLineRepository;
    private final BusCatalogApiHttpAdapter apiHttpAdapter;
    private final Logger logger = LoggerFactory.getLogger(SynchronizationService.class);

    public SynchronizationService(
        SynchronizationRepository synchronizationRepository,
        SynchronizedLineRepository synchronizedLineRepository,
        BusCatalogApiHttpAdapter apiHttpAdapter) {
            this.synchronizationRepository = synchronizationRepository;
            this.synchronizedLineRepository = synchronizedLineRepository;
            this.apiHttpAdapter = apiHttpAdapter;
    }

    public List<Synchronization> getAll() {
        var synchronizations = synchronizationRepository.findAll();
        logger.info(ServiceMessages.FETCHED_ALL_SYNCS, synchronizations.size());

        return synchronizations;
    }

    public void persist(List<Line> lines) {
        var synchronization = new Synchronization(SyncType.REST_API);
        synchronizationRepository.save(synchronization);

        for (var line : lines) {
            try {
                apiHttpAdapter.save(line);
                logger.info(ServiceMessages.SAVED_SYNC_DATA_AT_API, line.getIdentification());
                persistSynchronization(line, synchronization);
            } catch (HttpServerErrorException e) {
                logger.warn(
                    ServiceMessages.SAVED_SYNC_DATA_AT_API_FAILURE,
                    line.getIdentification(),
                    e.getStatusCode(),
                    e.getResponseBodyAsString());
            } catch (Exception e) {
                logger.warn(ServiceMessages.SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE,
                    line.getIdentification(),
                    e.getMessage());
            }
        }
    }

    private void persistSynchronization(Line line, Synchronization synchronization) {
        var synchronizedLine = new SynchronizedLine(line.getIdentification(), synchronization);
        synchronizedLineRepository.save(synchronizedLine);
        synchronization.setSuccessful(true);
        synchronizationRepository.save(synchronization);
        logger.info(ServiceMessages.SAVED_SYNC_AT_SCRAPER, line.getIdentification());
    }
}
