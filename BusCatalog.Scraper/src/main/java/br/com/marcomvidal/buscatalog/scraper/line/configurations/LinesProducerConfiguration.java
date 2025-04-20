package br.com.marcomvidal.buscatalog.scraper.line.configurations;

import java.util.Map;

import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.kafka.common.serialization.StringSerializer;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.kafka.core.DefaultKafkaProducerFactory;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.kafka.support.serializer.JsonSerializer;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;
import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.KafkaSection;
import br.com.marcomvidal.buscatalog.scraper.line.Line;

@Configuration
public class LinesProducerConfiguration {
    private final KafkaSection configuration;

    public LinesProducerConfiguration(ConfigurationSection configuration) {
        this.configuration = configuration.linesProducer;
    }

    @Bean
    public KafkaTemplate<String, Line> generate() {
        return new KafkaTemplate<>(
            new DefaultKafkaProducerFactory<>(
                Map.of(
                    ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, configuration.getBootstrapServers(),
                    ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, StringSerializer.class,
                    ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, JsonSerializer.class)));
    }
}
