package br.com.marcomvidal.buscatalog.scraper.line.services;

import java.util.HashMap;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;
import br.com.marcomvidal.buscatalog.scraper.line.Line;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.SyncType;
import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;
import br.com.marcomvidal.buscatalog.scraper.synchronization.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.synchronization.repositories.SynchronizationRepository;

@Service
public class LineProducerService {
    private final SynchronizationRepository synchronizationRepository;
    private final LineSyncService lineSyncService;
    private final KafkaTemplate<String, Line> producer;
    private final String topic;
    private final Logger logger = LoggerFactory.getLogger(LineProducerService.class);

    public LineProducerService(
        SynchronizationRepository synchronizationRepository,
        LineSyncService lineSyncService,
        KafkaTemplate<String, Line> producer,
        ConfigurationSection configuration) {
            this.lineSyncService = lineSyncService;
            this.synchronizationRepository = synchronizationRepository;
            this.producer = producer;
            this.topic = configuration.linesProducer.getTopic();
    }

    public HashMap<String, String> sync(List<Line> lines) {
        var synchronization = new Synchronization(SyncType.KAFKA_TOPIC);
        synchronizationRepository.save(synchronization);
        var errors = new HashMap<String, String>();

        for (var line : lines) {
            try {
                producer.send(topic, line)
                    .whenComplete((result, exception) -> {
                        if (exception != null) {
                            handleFailure(errors, exception);
                            return;
                        }

                        handleSuccess(line, synchronization);
                    });
            } catch (Exception e) {
                handleFailure(errors, e);
            }
            
        }

        return errors;
    }

    private void handleSuccess(Line line, Synchronization synchronization) {
        logger.info(
            ServiceMessages.SAVED_SYNC_DATA_AT_API.getMessage(),
            line.getIdentification());
        
        lineSyncService.persist(line, synchronization);
    }

    private void handleFailure(HashMap<String, String> errors, Throwable exception) {
        lineSyncService.registerError(
            errors,
            ServiceMessages.SAVED_SYNC_DATA_AT_API_UNEXPECTED_FAILURE,
            exception.getMessage());
    }
}
