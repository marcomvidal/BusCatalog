using FluentAssertions;
using SantoAndreOnBus.Api.Companies.DTOs;
using SantoAndreOnBus.Test.Fixtures;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Companies;

public class Put : IntegrationTest
{
    [Fact]
    public async void WhenItUpdatesAValidCompany_ShouldRespondWithIt()
    {
        await Context.Companies.AddAsync(CompanyFakes.Companies[0]);
        await Context.SaveChangesAsync();

        var request = new CompanySubmitRequest
        {
            Name = "Valid Company With Another Name",
            Prefixes = new [] { "C", "D" }
        };

        var response = await Client.PutAsJsonAsync("/api/companies/1", request);
        var body = await response.DeserializedBody<CompanyResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<CompanyResponse>(x => x.Data!.Name == request.Name);
    }

    [Fact]
    public async void WhenItPutsACompanyThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new CompanySubmitRequest
        {
            Name = "Valid Company With Another Name",
            Prefixes = new [] { "C", "D" }
        };

        var response = await Client.PutAsJsonAsync("/api/companies/0", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenItPutsAnInvalidCompany_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PutAsJsonAsync("/api/companies/1", new CompanySubmitRequest());
        var body = await response.DeserializedBody<CompanyResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Name");
    }
}