namespace BusCatalog.Api.Domain.Vehicles.Ports;

public record VehiclePostRequest
{
    public string? Identification { get; set; }
    public string? Description { get; set; }
}