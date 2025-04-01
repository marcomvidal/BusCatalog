package br.com.marcomvidal.buscatalog.scraper.api.ports;

import java.util.List;

import lombok.Getter;
import lombok.Setter;

public class SaveLineResponse {
    @Getter
    @Setter
    public int id;

    @Getter
    @Setter
    public String identification;

    @Getter
    @Setter
    public String fromwards;

    @Getter
    @Setter
    public String towards;

    @Getter
    @Setter
    public int departuresPerDay;

    @Getter
    @Setter
    public List<SaveVehicleResponse> vehicles;
}
