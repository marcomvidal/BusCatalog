namespace BusCatalog.Api.Domain.Places;

public record PlacePostRequest
{
    public string? Identification { get; set; }
    public string? City { get; set; }
}
