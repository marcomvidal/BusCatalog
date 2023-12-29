using AutoMapper;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

public class VehicleMapper : Profile
{
    public VehicleMapper() => CreateMap<VehiclePostRequest, Vehicle>();
}
