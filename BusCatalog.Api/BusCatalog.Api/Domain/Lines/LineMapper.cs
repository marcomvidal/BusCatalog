using AutoMapper;

namespace BusCatalog.Api.Domain.Lines;

public class LineMapper : Profile
{
    public LineMapper()
    {
        CreateMap<LinePostRequest, Line>()
            .ForMember(dst => dst.Vehicles, src => src.Ignore());

        CreateMap<Line, LineResponse>()
            .ForMember(
                dst => dst.Vehicles,
                src => src.MapFrom(map => map.Vehicles.Select(x => x.Identification)));
    }
}
