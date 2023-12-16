using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record VehiclePostRequest
{
    public string Identification { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string NormalizedIdentification
    {
        get => Identification is not null
            ? Identification.ToUpper().Replace(" ", "_")
            : string.Empty;
    }
}