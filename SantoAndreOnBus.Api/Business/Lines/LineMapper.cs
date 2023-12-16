using AutoMapper;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LineMapper : Profile
{
    public LineMapper() => CreateMap<LinePostRequest, Line>();
}


