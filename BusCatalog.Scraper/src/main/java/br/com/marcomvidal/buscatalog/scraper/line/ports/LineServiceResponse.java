package br.com.marcomvidal.buscatalog.scraper.line.ports;

import java.util.Map;
import java.util.Map.Entry;
import java.util.Optional;

import lombok.Getter;

@Getter
public class LineServiceResponse<T> {
    @Getter
    private final Optional<T> data;
    
    private final Optional<Entry<String, String>> error;

    public LineServiceResponse(T data) {
        this.data = Optional.of(data);
        this.error = Optional.empty();
    }

    public LineServiceResponse(String errorKey, String errorDescription) {
        this.data = Optional.empty();
        this.error = Optional.of(Map.entry(errorKey, errorDescription));
    }
}
