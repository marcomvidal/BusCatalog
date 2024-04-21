package br.com.marcomvidal.buscatalog.scraper.line.ports;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map.Entry;

import br.com.marcomvidal.buscatalog.scraper.line.Line;
import lombok.Getter;

@Getter
public class LineResponse {
    private List<Line> lines = new ArrayList<Line>();
    private HashMap<String, String> errors = new HashMap<>();

    public void addLineOrError(LineServiceResponse<Line> line) {
        line.getError().ifPresentOrElse(
            error -> addError(error),
            () -> lines.add(line.getResponse().get()));
    }

    public void addError(Entry<String, String> error) {
        errors.put(error.getKey(), error.getValue());
    }

    public boolean hasLines() {
        return !lines.isEmpty();
    }
}
