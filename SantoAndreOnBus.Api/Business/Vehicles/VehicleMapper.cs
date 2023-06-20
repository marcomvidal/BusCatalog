using AutoMapper;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public class VehicleMapper : Profile
{
    public VehicleMapper() =>
        CreateMap<VehicleSubmitRequest, Vehicle>()
            .ForMember(
                dest => dest.Identification,
                opt => opt.MapFrom(src => src.NormalizedIdentification));
}


