package br.com.marcomvidal.buscatalog.scraper;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

import br.com.marcomvidal.buscatalog.scraper.emtu.EmtuConfiguration;

@SpringBootApplication
@EnableConfigurationProperties(EmtuConfiguration.class)
public class ScraperApplication {

	public static void main(String[] args) {
		SpringApplication.run(ScraperApplication.class, args);
	}
}
