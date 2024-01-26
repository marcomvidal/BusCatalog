using AutoMapper;

namespace BusCatalog.Api.Domain.Vehicles;

public class VehicleMapper : Profile
{
    public VehicleMapper() => CreateMap<VehiclePostRequest, Vehicle>();
}
