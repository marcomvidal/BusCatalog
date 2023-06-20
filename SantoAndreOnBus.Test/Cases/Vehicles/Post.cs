using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SantoAndreOnBus.Api.Business.Vehicles;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Post : IntegrationTest
{
    [Fact]
    public async void WhenItPostsAValidCompany_ShouldRespondWithIt()
    {
        var request = new VehicleSubmitRequest
        {
            Identification = "ARTICULATED",
            Description = "Articulated"
        };

        var response = await Client.PostAsJsonAsync("/api/vehicles", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.DeserializedBody<VehicleResponse>())
            .Should()
            .Match<VehicleResponse>(x =>
                x.Data!.Identification == request.Identification
                && x.Data!.Description== request.Description);
    }

    [Fact]
    public async void WhenItPostsAnInvalidCompany_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/vehicles", new VehicleSubmitRequest());
        var body = await response.DeserializedBody<VehicleResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}