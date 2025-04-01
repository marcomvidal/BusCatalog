package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import br.com.marcomvidal.buscatalog.scraper.emtu.services.EmtuLineDenominationService;
import br.com.marcomvidal.buscatalog.scraper.emtu.services.EmtuLineDataService;
import br.com.marcomvidal.buscatalog.scraper.line.messages.ServiceMessages;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineServiceResponse;

@Service
public class LineService {
    private final EmtuLineDenominationService denominationService;
    private final EmtuLineDataService dataService;
    private final Logger logger = LoggerFactory.getLogger(LineService.class);

    public LineService(
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
            logger.warn(ServiceMessages.LINE_QUERY_FAILED.getMessage(), denomination.getData().get());
            return response.withError(denomination.getError().get());
        }
        
        var data = dataService.get(denomination.getData().get());
        
        logger.info(
            ServiceMessages.LINE_QUERY_CONCLUDED.getMessage(),
            denomination.getData().get());

        return response.withDataOrError(data);
    }
}
