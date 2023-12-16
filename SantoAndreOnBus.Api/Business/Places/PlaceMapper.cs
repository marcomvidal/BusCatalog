using AutoMapper;

namespace SantoAndreOnBus.Api.Business.Places;

public class PlaceMapper : Profile
{
    public PlaceMapper() => CreateMap<PlacePostRequest, Place>();
}
