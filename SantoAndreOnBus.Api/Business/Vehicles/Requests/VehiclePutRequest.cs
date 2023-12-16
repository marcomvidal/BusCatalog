using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record VehiclePutRequest : VehiclePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}