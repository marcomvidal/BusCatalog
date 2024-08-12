package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.data.EmtuLineDataService;
import br.com.marcomvidal.buscatalog.scraper.emtu.denomination.EmtuLineDenominationService;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class LineQueryService {
    private final EmtuLineDenominationService denominationService;
    private final EmtuLineDataService dataService;

    public LineQueryService(
        EmtuLineDenominationService denominationService,
        EmtuLineDataService dataService) {
        this.denominationService = denominationService;
        this.dataService = dataService;
    }

    public LineResponse query(List<String> identifiers) {
        var response = new LineResponse();

        identifiers.forEach(identifier -> {
            var denomination = denominationService.query(identifier);
            queryData(denomination, response);
        });

        return response;
    }

    private LineResponse queryData(
        LineServiceResponse<String> denomination,
        LineResponse response) {
        if (denomination.getError().isPresent()) {
            return response.withError(denomination.getError().get());
        }
        
        var data = dataService.get(denomination.getData().get());

        return response.withDataOrError(data);
    }
}
