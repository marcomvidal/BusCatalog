using BusCatalog.Api.Extensions;

namespace BusCatalog.Api.Domain.Vehicles;

public record VehiclePostRequest
{
    private string _identification = string.Empty;

    public string? Identification
    {
        get => _identification.SlugfyUpper();
        set => _identification = value is not null ? value : string.Empty;
    }

    public string Description { get; set; } = string.Empty;
}