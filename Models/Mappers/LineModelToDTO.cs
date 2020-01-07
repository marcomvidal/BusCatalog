using System.Linq;
using AutoMapper;
using SantoAndreOnBus.Models.DTOs;

namespace SantoAndreOnBus.Models.Mappers
{
    public class LineModelToDTO : Profile
    {
        public LineModelToDTO() => CreateMap<Line, LineDTO>()
            .ForMember(
                dest => dest.Vehicles, 
                opt => opt.MapFrom(from => 
                from.LineVehicles.Select(v => new Vehicle {
                    Id = v.VehicleId,
                    Name = v.Vehicle.Name
                }))
            );
    }
}