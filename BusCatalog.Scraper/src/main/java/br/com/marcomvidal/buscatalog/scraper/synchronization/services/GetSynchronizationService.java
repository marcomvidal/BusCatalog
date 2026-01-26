package br.com.marcomvidal.buscatalog.scraper.synchronization.services;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;

@Service
public class GetSynchronizationService {
    private final SynchronizationRepository synchronizationRepository;
    private final Logger logger = LoggerFactory.getLogger(GetSynchronizationService.class);

    public GetSynchronizationService(SynchronizationRepository synchronizationRepository) {
        this.synchronizationRepository = synchronizationRepository;
    }

    public List<Synchronization> getAll() {
        var synchronizations = synchronizationRepository.findAll();
        logger.info(ServiceMessages.FETCHED_ALL_SYNCS.getMessage(), synchronizations.size());

        return synchronizations;
    }
}
