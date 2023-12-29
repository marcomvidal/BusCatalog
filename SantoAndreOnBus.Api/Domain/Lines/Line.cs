using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Api.Domain.Lines;

public record Line
{
    public int Id { get; set; }
    public string Identification { get; set; } = null!;
    public string? Fromwards { get; set; } = null!;
    public string? Towards { get; set; } = null!;
    public int DeparturesPerDay { get; set; }
    public virtual List<Place> Places { get; set; } = [];
    public virtual List<Vehicle> Vehicles { get; set; } = [];

    public void AddPlaces(IEnumerable<Place> places) =>
        Places.AddRange(places);

    public void AddVehicles(IEnumerable<Vehicle> vehicles) =>
        Vehicles.AddRange(vehicles);
}
