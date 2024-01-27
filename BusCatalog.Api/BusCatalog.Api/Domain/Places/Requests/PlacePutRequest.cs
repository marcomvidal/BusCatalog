using System.Text.Json.Serialization;

namespace BusCatalog.Api.Domain.Places;

public record PlacePutRequest : PlacePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}
