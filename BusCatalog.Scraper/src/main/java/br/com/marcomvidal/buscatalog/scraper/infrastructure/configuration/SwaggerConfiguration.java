package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.ConfigurationSection;
import br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration.sections.DescriptionSection;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;

@Configuration
public class SwaggerConfiguration {
    private final DescriptionSection section;

    public SwaggerConfiguration(ConfigurationSection configuration) {
        this.section = configuration.getDescription();
    }

    @Bean
    public OpenAPI define() {
        return new OpenAPI().info(
            new Info()
                .title(section.getTitle())
                .version(section.getVersion())
                .description(section.getDescription()));
    }
}
