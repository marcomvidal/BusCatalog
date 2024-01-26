using AutoMapper;

namespace BusCatalog.Api.Domain.Lines;

public class LineMapper : Profile
{
    public LineMapper() =>
        CreateMap<LinePostRequest, Line>()
            .ForMember(dst => dst.Vehicles, src => src.Ignore())
            .ForMember(dst => dst.Places, src => src.Ignore());
}
