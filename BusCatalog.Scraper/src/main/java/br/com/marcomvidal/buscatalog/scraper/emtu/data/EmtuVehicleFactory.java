package br.com.marcomvidal.buscatalog.scraper.emtu.data;

import java.util.List;
import java.util.Map;
import java.util.Optional;

import br.com.marcomvidal.buscatalog.scraper.line.VehicleType;

public class EmtuVehicleFactory {
    private static Map<String, List<VehicleType>> data = Map.ofEntries(
        Map.entry("284", List.of(VehicleType.ARTICULADO, VehicleType.PADRON)),
        Map.entry("285", List.of(VehicleType.SUPER_ARTICULADO, VehicleType.ARTICULADO)),
        Map.entry("286", List.of(VehicleType.ARTICULADO, VehicleType.PADRON)),
        Map.entry("287", List.of(VehicleType.SUPER_ARTICULADO, VehicleType.ARTICULADO)),
        Map.entry("287P", List.of(VehicleType.PADRON)),
        Map.entry("288", List.of(VehicleType.SUPER_ARTICULADO, VehicleType.ARTICULADO)),
        Map.entry("288P", List.of(VehicleType.PADRON)),
        Map.entry("289", List.of(VehicleType.ARTICULADO, VehicleType.PADRON)),
        Map.entry("290", List.of(VehicleType.SUPER_ARTICULADO, VehicleType.ARTICULADO)),
        Map.entry("376", List.of(VehicleType.SUPER_ARTICULADO, VehicleType.ARTICULADO))
    );

    public static List<String> generate(String lineName) {
        return Optional
            .ofNullable(data.get(lineName))
            .orElse(List.of(VehicleType.PADRON))
            .stream()
            .map(VehicleType::toString)
            .toList();
    }
}
