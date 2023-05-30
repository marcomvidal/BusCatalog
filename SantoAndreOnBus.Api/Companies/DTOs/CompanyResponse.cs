using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Companies.DTOs;

public record CompanyResponse : Response<Company>
{
    public CompanyResponse() {}

    public CompanyResponse(Company? data) : base(data) {}
}
