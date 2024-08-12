package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration;

import org.springframework.boot.context.properties.ConfigurationProperties;

import br.com.marcomvidal.buscatalog.scraper.api.BusCatalogApiConfiguration;
import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuConfiguration;
import lombok.Getter;
import lombok.Setter;

@ConfigurationProperties(prefix = "configuration")
@Getter
@Setter
public class Configuration {
    public BusCatalogApiConfiguration busCatalog;
    public EmtuConfiguration emtu;
}
