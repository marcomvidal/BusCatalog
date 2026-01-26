package br.com.marcomvidal.buscatalog.scraper.line;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class Line {
    private String identification;
    private String fromwards;
    private String towards;
    private int departuresPerDay;
    private List<String> vehicles;

    public Line(
        String identification,
        String fromwards,
        String towards,
        int departuresPerDay) {
        this.identification = identification;
        this.fromwards = fromwards;
        this.towards = towards;
        this.departuresPerDay = departuresPerDay;
    }

    public Line withVehicles(List<String> vehicles) {
        this.vehicles = vehicles;

        return this;
    }
}
