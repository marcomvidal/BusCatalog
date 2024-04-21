package br.com.marcomvidal.buscatalog.scraper.emtu;

import org.springframework.boot.context.properties.ConfigurationProperties;

import lombok.Getter;
import lombok.Setter;

@ConfigurationProperties(prefix = "emtu")
@Getter
@Setter
public class EmtuConfiguration {
    public String identificationUrl;
    public String dataUrl;
}
