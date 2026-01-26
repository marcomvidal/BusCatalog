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

    public LineResponse withDataOrError(LineServiceResponse<Line> response) {
        response.getError().ifPresentOrElse(
            error -> withError(error),
            () -> lines.add(response.getData().get()));

        return this;
    }

    public LineResponse withError(Entry<String, String> error) {
        errors.put(error.getKey(), error.getValue());

        return this;
    }

    public LineResponse withErrors(HashMap<String, String> errors) {
        this.errors.putAll(errors);

        return this;
    }

    public boolean hasLines() {
        return !lines.isEmpty();
    }
}
