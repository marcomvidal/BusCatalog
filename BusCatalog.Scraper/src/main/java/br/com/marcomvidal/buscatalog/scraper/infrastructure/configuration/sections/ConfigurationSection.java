package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections;

import org.springframework.boot.context.properties.ConfigurationProperties;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuConfiguration;
import lombok.Getter;
import lombok.Setter;

@ConfigurationProperties(prefix = "configuration")
@Getter
@Setter
public class ConfigurationSection {
    private String apiUrl;
    private String spaUrl;
    private EmtuConfiguration emtu;
    private KafkaSection linesProducer;
    private DescriptionSection description;
}
