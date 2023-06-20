using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.General;
using SantoAndreOnBus.Test.Fakes;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Companies;

public class Delete : IntegrationTest
{
    [Fact]
    public async void WhenTheCompanyExists_ShouldDeleteSuccessfully()
    {
        var id = 1;
        await Context.Companies.AddAsync(CompanyFakes.Companies[0]);
        await Context.SaveChangesAsync();

        var response = await Client.DeleteAsync($"/api/companies/{id}");
        var body = await response.DeserializedBody<DeleteResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<DeleteResponse>(x => x.Data!.Deleted == id);
    }

    [Fact]
    public async void WhenTheCompanyDoesNotExists_ShouldReturnNotFound()
    {
        var response = await Client.DeleteAsync($"/api/companies/0");
        var body = await response.DeserializedBody<DeleteResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
