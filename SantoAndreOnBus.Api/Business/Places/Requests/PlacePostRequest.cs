namespace SantoAndreOnBus.Api.Business.Places;

public record PlacePostRequest
{
    public string Identification { get; set; } = null!;
    public string City { get; set; } = null!;
}
