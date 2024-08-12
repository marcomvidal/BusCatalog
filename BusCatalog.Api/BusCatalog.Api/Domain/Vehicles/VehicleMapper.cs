using AutoMapper;
using BusCatalog.Api.Domain.Vehicles.Ports;

namespace BusCatalog.Api.Domain.Vehicles;

public class VehicleMapper : Profile
{
    public VehicleMapper() => CreateMap<VehiclePostRequest, Vehicle>();
}
