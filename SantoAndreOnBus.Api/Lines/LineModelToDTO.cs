using AutoMapper;
using SantoAndreOnBus.Api.Vehicles;

namespace SantoAndreOnBus.Api.Lines;

public class LineModelToDTO : Profile
{
    public LineModelToDTO() => CreateMap<Line, LineSubmitRequest>()
        .ForMember(
            dest => dest.Vehicles, 
            opt => opt.MapFrom(from => 
            from.LineVehicles.Select(v => new Vehicle {
                Id = v.VehicleId,
                Name = v.Vehicle.Name
            }))
        );
}
