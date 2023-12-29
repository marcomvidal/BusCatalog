namespace SantoAndreOnBus.Api.Domain.Places;

public record PlacePostRequest
{
    public string Identification { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
