using AutoMapper;

namespace SantoAndreOnBus.Api.Business.Companies;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<CompanySubmitRequest, Company>()
            .ForMember(
                dest => dest.Prefixes,
                opt => opt.MapFrom(
                    src => src.Prefixes.Select(x => new Prefix { Identification = x })));
    }
}
