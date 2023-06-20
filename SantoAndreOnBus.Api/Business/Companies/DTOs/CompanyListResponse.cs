using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Companies;

public record CompanyListResponse : Response<IEnumerable<Company>>
{
    public CompanyListResponse() {}

    public CompanyListResponse(IEnumerable<Company>? data) : base(data) {}
}