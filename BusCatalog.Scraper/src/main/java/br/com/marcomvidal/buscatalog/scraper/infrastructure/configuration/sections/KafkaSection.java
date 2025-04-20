package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class KafkaSection {
    private String bootstrapServers;
    private String topic;
}
