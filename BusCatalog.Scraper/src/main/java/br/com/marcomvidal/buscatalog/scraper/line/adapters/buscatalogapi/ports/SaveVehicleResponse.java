package br.com.marcomvidal.buscatalog.scraper.line.adapters.buscatalogapi.ports;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class SaveVehicleResponse {
    public int id;
    public String identification;
    public String description;
}
