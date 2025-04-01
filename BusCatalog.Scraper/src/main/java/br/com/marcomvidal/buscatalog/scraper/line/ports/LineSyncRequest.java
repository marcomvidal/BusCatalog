package br.com.marcomvidal.buscatalog.scraper.line.ports;

import java.util.List;

import lombok.Getter;
import lombok.Setter;

public class LineSyncRequest {
    @Getter
    @Setter
    private List<String> lines;
}
