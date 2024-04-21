package br.com.marcomvidal.buscatalog.scraper.emtu;

import java.util.List;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import br.com.marcomvidal.buscatalog.scraper.line.LineService;
import br.com.marcomvidal.buscatalog.scraper.line.ports.LineResponse;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

@Tag(name = "Emtu")
@RestController
@RequestMapping("/api/emtu")
public class EmtuController {
    private final LineService service;

    public EmtuController(LineService service) {
        this.service = service;
    }

    @Operation(summary = "Get data from the official source for a list of lines.")
    @ApiResponses(value = {
        @ApiResponse(responseCode = "200", description = "Successfully retrieved data"),
        @ApiResponse(responseCode = "400", description = "Invalid line list")
    })
    @GetMapping
    public ResponseEntity<LineResponse> get(@RequestParam List<String> lines) {
        var response = this.service.get(lines);

        return new ResponseEntity<>(
            response,
            response.hasLines() ? HttpStatus.OK : HttpStatus.BAD_REQUEST);
    }
}
