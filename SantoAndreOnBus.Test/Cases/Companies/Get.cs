using System.Linq;
using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Test.Fakes;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Companies;

public class Get : IntegrationTest
{
    [Fact]
    public async void WhenItHasNoCompanies_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/companies");
        var body = await response.DeserializedBody<CompanyListResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Data.Should().BeEmpty();
    }

    [Fact]
    public async void WhenItHasCompanies_ShouldRespondWithIt()
    {
        var companies = new Company[] { CompanyFakes.Companies[0], CompanyFakes.Companies[1] };
        await Context.Companies.AddRangeAsync(companies);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync("/api/companies");
        var body = await response.DeserializedBody<CompanyListResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Data!.Select(x => x.Name).Should().BeEquivalentTo(companies.Select(x => x.Name));
        body!.Data!.Select(x => x.Prefixes).Should().NotBeEmpty();
    }
}