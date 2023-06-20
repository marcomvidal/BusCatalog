using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record VehicleListResponse : Response<IEnumerable<Vehicle>>
{
    public VehicleListResponse(IEnumerable<Vehicle>? data) : base(data) {}
}
