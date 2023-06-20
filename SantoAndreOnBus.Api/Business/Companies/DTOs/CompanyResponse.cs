using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Companies;

public record CompanyResponse : Response<Company>
{
    public CompanyResponse() {}

    public CompanyResponse(Company? data) : base(data) {}
}
