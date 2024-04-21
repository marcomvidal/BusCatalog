package br.com.marcomvidal.buscatalog.scraper.line.ports;

import java.util.Map;
import java.util.Map.Entry;
import java.util.Optional;

import lombok.Getter;

@Getter
public class LineServiceResponse<T> {
    private final Optional<T> response;
    private final Optional<Entry<String, String>> error;

    public LineServiceResponse(T response) {
        this.response = Optional.of(response);
        this.error = Optional.empty();
    }

    public LineServiceResponse(String errorKey, String errorDescription) {
        this.response = Optional.empty();
        this.error = Optional.of(Map.entry(errorKey, errorDescription));
    }
}
