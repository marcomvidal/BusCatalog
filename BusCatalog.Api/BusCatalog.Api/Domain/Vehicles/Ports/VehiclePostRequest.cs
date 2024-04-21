namespace BusCatalog.Api.Domain.Vehicles;

public record VehiclePostRequest
{
    public string? Identification { get; set; }
    public string? Description { get; set; }
}