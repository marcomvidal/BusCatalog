using AutoMapper;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LineModelToDTO : Profile
{
    public LineModelToDTO() => CreateMap<Line, LineSubmitRequest>()
        .ForMember(
            dest => dest.Vehicles, 
            opt => opt.MapFrom(from => 
            from.Vehicles!.Select(v => new Vehicle {
                Id = v.Id,
                Identification = v.Identification
            }))
        );
}
