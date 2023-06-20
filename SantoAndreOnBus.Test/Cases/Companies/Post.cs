using FluentAssertions;
using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Test.Fixtures;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Companies;

public class Post : IntegrationTest
{
    [Fact]
    public async void WhenItPostsAValidCompany_ShouldRespondWithIt()
    {
        var request = new CompanySubmitRequest
        {
            Name = "Valid Company",
            Prefixes = new [] { "A", "B" }
        };

        var response = await Client.PostAsJsonAsync("/api/companies", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.DeserializedBody<CompanyResponse>())
            .Should()
            .Match<CompanyResponse>(x =>
                x.Data!.Name == request.Name
                && x.Data!.Prefixes.Count == request.Prefixes.Count());
    }

    [Fact]
    public async void WhenItPostsAnInvalidCompany_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/companies", new CompanySubmitRequest());
        var body = await response.DeserializedBody<CompanyResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Name");
    }
}