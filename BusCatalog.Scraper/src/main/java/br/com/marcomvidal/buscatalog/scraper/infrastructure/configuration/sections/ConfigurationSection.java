package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections;

import org.springframework.boot.context.properties.ConfigurationProperties;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuConfiguration;
import lombok.Getter;
import lombok.Setter;

@ConfigurationProperties(prefix = "configuration")
@Getter
@Setter
public class ConfigurationSection {
    public String apiUrl;
    public String spaUrl;
    public EmtuConfiguration emtu;
    public KafkaSection linesProducer;
}
