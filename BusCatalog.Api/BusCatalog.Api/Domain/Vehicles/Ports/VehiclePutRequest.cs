using System.Text.Json.Serialization;

namespace BusCatalog.Api.Domain.Vehicles.Ports;

public sealed record VehiclePutRequest : VehiclePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}