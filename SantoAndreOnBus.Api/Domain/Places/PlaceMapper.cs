using AutoMapper;

namespace SantoAndreOnBus.Api.Domain.Places;

public class PlaceMapper : Profile
{
    public PlaceMapper() => CreateMap<PlacePostRequest, Place>();
}
