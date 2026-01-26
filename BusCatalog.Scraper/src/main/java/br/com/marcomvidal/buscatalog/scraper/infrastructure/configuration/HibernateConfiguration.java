package br.com.marcomvidal.buscatalog.scraper.infrastructure.configuration;

import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.env.Environment;
import org.springframework.jdbc.datasource.DriverManagerDataSource;

@Configuration
public class HibernateConfiguration {
    private static final String CONFIGURATION_SECTION = "spring.datasource.";

    @Autowired
    private Environment environment;

    @Bean
    public DataSource dataSource() {
        final DriverManagerDataSource dataSource = new DriverManagerDataSource();
        dataSource.setDriverClassName(getProperty("driverClassName"));
        dataSource.setUrl(getProperty("url"));
        dataSource.setUsername(getProperty("user"));
        dataSource.setPassword(getProperty("password"));

        return dataSource;
    }

    private String getProperty(String propertyName) {
        return environment.getProperty(CONFIGURATION_SECTION + propertyName);
    }
}
