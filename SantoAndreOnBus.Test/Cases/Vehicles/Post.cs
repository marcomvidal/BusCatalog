using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Domain.Vehicles;
using SantoAndreOnBus.Api.Extensions;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Post(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenItPostsAValidVehicle_ShouldRespondWithIt()
    {
        var request = new VehiclePostRequest
        {
            Identification = "super articulated",
            Description = "Articulated"
        };

        var response = await Client.PostAsJsonAsync("/api/vehicles", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Vehicle>())
            .Should()
            .Match<Vehicle>(x =>
                x.Identification == request.Identification.SlugfyUpper()
                && x.Description == request.Description);
    }

    [Fact]
    public async void WhenItPostsAnEmptyVehicle_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/vehicles", new VehiclePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification", "Description");
    }

    [Fact]
    public async void WhenItPostsAVehicleWithRepeatedIdentification_ShouldRespondWithIt()
    {
        var request = new VehiclePostRequest
        {
            Identification = "PADRON",
            Description = "Padron"
        };

        await Client.PostAsJsonAsync("/api/vehicles", request);
        var response = await Client.PostAsJsonAsync("/api/vehicles", request);
        var body = await response.DeserializedBody<ValidationProblemDetails>();
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}