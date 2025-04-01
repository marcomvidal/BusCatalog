package br.com.marcomvidal.buscatalog.scraper.api.ports;

import lombok.Getter;
import lombok.Setter;

public class SaveVehicleResponse {
    @Getter
    @Setter
    public int id;

    @Getter
    @Setter
    public String identification;

    @Getter
    @Setter
    public String description;
}
