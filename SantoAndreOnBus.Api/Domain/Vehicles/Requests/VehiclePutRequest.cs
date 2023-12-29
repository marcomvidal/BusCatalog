using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

public record VehiclePutRequest : VehiclePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}