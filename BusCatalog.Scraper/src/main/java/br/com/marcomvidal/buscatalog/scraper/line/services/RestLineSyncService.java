package br.com.marcomvidal.buscatalog.scraper.line.services;

import java.util.HashMap;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.web.client.HttpServerErrorException;

import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.line.adapters.buscatalogapi.ApiLinesAdapter;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SyncType;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;

@Service
public class RestLineSyncService {
    private final SynchronizationRepository synchronizationRepository;
    private final LineSyncService lineSyncService;
    private final ApiLinesAdapter apiLinesAdapter;
    private final Logger logger = LoggerFactory.getLogger(RestLineSyncService.class);

    public RestLineSyncService(
        LineSyncService lineSyncService,
        SynchronizationRepository synchronizationRepository,
        ApiLinesAdapter apiLinesAdapter) {
            this.lineSyncService = lineSyncService;
            this.synchronizationRepository = synchronizationRepository;
            this.apiLinesAdapter = apiLinesAdapter;
    }

    public HashMap<String, String> sync(List<Line> lines) {
        var synchronization = new Synchronization(SyncType.REST_API);
        synchronizationRepository.save(synchronization);
        var errors = new HashMap<String, String>();

        for (var line : lines) {
            try {
                handleSuccess(line, synchronization);
            } catch (HttpServerErrorException e) {
                handleHttpResponseError(errors, e);
            } catch (Exception e) {
                handleUnexpectedError(errors, e);
            }
        }

        return errors;
    }

    private void handleSuccess(Line line, Synchronization synchronization) {
        apiLinesAdapter.save(line);

        logger.info(
            ServiceMessages.SAVED_SYNC_DATA_AT_API.getMessage(),
            line.getIdentification());
        
        lineSyncService.persist(line, synchronization);
    }

    private void handleHttpResponseError(HashMap<String, String> errors, HttpServerErrorException e) {
        lineSyncService.registerError(
            errors,
            ServiceMessages.SAVED_SYNC_DATA_AT_API_FAILURE,
            e.getResponseBodyAsString());
    }

    private void handleUnexpectedError(HashMap<String, String> errors, Exception e) {
        lineSyncService.registerError(
            errors,
            ServiceMessages.SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE,
            e.getMessage());
    }
}
