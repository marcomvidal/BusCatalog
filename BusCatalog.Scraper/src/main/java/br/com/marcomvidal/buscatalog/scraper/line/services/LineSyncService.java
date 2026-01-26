package br.com.marcomvidal.buscatalog.scraper.line.services;

import java.util.HashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SynchronizedLine;
import br.com.marcomvidal.buscatalog.scraper.synchronization.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizedLineRepository;

@Service
public class LineSyncService {
    private final SynchronizationRepository synchronizationRepository;
    private final SynchronizedLineRepository synchronizedLineRepository;
    private final Logger logger = LoggerFactory.getLogger(LineSyncService.class);

    public LineSyncService(
        SynchronizationRepository synchronizationRepository,
        SynchronizedLineRepository synchronizedLineRepository) {
        this.synchronizationRepository = synchronizationRepository;
        this.synchronizedLineRepository = synchronizedLineRepository;
    }

    public void persist(Line line, Synchronization synchronization) {
        var synchronizedLine = new SynchronizedLine(line.getIdentification(), synchronization);
        synchronizedLineRepository.save(synchronizedLine);
        synchronization.setSuccessful(true);
        synchronizationRepository.save(synchronization);
        logger.info(ServiceMessages.SAVED_SYNC_AT_SCRAPER.getMessage(), line.getIdentification());
    }

    public void registerError(
        HashMap<String, String> errors,
        ServiceMessages message,
        String description) {
        logger.warn(message.getMessage(), description);
        errors.put(message.name(), description);
    }
}
