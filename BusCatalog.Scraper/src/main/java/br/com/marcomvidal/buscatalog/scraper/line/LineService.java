package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.data.EmtuLineDataService;
import br.com.marcomvidal.buscatalog.scraper.emtu.denomination.EmtuLineDenominationService;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;

@Service
public class LineService {
    private final EmtuLineDenominationService denominationService;
    private final EmtuLineDataService dataService;

    public LineService(
        EmtuLineDenominationService denominationService,
        EmtuLineDataService dataService) {
        this.denominationService = denominationService;
        this.dataService = dataService;
    }

    public LineResponse get(List<String> identifiers) {
        var response = new LineResponse();

        identifiers.forEach(identifier -> {
            var denomination = denominationService.get(identifier);

            denomination.getError().ifPresentOrElse(
                error -> response.addError(error),
                () -> {
                    var line = dataService.get(denomination.getResponse().get());
                    response.addLineOrError(line);
                });
        });

        return response;
    }
}
