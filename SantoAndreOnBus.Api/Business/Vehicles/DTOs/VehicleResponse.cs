using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record VehicleResponse : Response<Vehicle>
{
    public VehicleResponse() {}

    public VehicleResponse(Vehicle? data) => Data = data;
}
