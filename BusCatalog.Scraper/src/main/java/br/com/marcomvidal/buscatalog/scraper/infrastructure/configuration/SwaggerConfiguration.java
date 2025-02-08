package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;

@Configuration
public class SwaggerConfiguration {
    @Bean
    public OpenAPI define() {
        var info = new Info()
            .title("BusCatalog.Scraper")
            .version("1.0")
            .description("This API fetches data from transport authorities and provides ways to synchronize them with BusCatalog.Api.");

        return new OpenAPI().info(info);
    }
}
