using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Companies.DTOs;

public record CompanyListResponse : Response<IEnumerable<Company>>
{
    public CompanyListResponse() {}

    public CompanyListResponse(IEnumerable<Company>? data) : base(data) {}
}