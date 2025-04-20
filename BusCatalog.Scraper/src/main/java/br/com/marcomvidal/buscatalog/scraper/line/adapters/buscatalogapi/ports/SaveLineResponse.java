package br.com.marcomvidal.buscatalog.scraper.line.adapters.buscatalogapi.ports;

import java.util.List;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class SaveLineResponse {
    public int id;
    public String identification;
    public String fromwards;
    public String towards;
    public int departuresPerDay;
    public List<SaveVehicleResponse> vehicles;
}
