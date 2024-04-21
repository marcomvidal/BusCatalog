using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public record Line
{
    public int Id { get; set; }
    public required string Identification { get; set; }
    public required string Fromwards { get; set; }
    public required string Towards { get; set; }
    public int DeparturesPerDay { get; set; }
    public virtual List<Place> Places { get; set; } = [];
    public virtual List<Vehicle> Vehicles { get; set; } = [];

    public void AddPlaces(IEnumerable<Place> places) =>
        Places.AddRange(places);

    public void AddVehicles(IEnumerable<Vehicle> vehicles) =>
        Vehicles.AddRange(vehicles);
}
