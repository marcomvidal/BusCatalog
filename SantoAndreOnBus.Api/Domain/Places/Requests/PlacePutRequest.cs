using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Domain.Places;

public record PlacePutRequest : PlacePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}
