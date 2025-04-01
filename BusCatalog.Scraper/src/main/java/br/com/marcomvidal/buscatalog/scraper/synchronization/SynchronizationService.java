package br.com.marcomvidal.buscatalog.scraper.synchronization;

import java.util.HashMap;
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
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizedLineRepository;

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
        logger.info(ServiceMessages.FETCHED_ALL_SYNCS.getMessage(), synchronizations.size());

        return synchronizations;
    }

    public HashMap<String, String> persist(List<Line> lines) {
        var synchronization = new Synchronization(SyncType.REST_API);
        synchronizationRepository.save(synchronization);
        var errors = new HashMap<String, String>();

        for (var line : lines) {
            try {
                apiHttpAdapter.save(line);
                logger.info(ServiceMessages.SAVED_SYNC_DATA_AT_API.getMessage(), line.getIdentification());
                persistSynchronization(line, synchronization);
            } catch (HttpServerErrorException e) {
                registerError(
                    errors,
                    ServiceMessages.SAVED_SYNC_DATA_AT_API_FAILURE,
                    e.getResponseBodyAsString());
            } catch (Exception e) {
                registerError(
                    errors,
                    ServiceMessages.SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE,
                    e.getMessage());
            }
        }

        return errors;
    }

    private void persistSynchronization(Line line, Synchronization synchronization) {
        var synchronizedLine = new SynchronizedLine(line.getIdentification(), synchronization);
        synchronizedLineRepository.save(synchronizedLine);
        synchronization.setSuccessful(true);
        synchronizationRepository.save(synchronization);
        logger.info(ServiceMessages.SAVED_SYNC_AT_SCRAPER.getMessage(), line.getIdentification());
    }

    private void registerError(
        HashMap<String, String> errors,
        ServiceMessages message,
        String description) {
        logger.warn(message.getMessage(), description);
        errors.put(message.name(), description);
    }
}
