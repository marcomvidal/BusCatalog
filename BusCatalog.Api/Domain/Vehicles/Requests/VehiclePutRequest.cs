using System.Text.Json.Serialization;

namespace BusCatalog.Api.Domain.Vehicles;

public record VehiclePutRequest : VehiclePostRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}