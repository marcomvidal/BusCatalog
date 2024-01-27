using AutoMapper;

namespace BusCatalog.Api.Domain.Places;

public class PlaceMapper : Profile
{
    public PlaceMapper() => CreateMap<PlacePostRequest, Place>();
}
