using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Places;

public class Post(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenItPostsAValidPlace_ShouldRespondWithIt()
    {
        var request = new PlacePostRequest
        {
            Identification = "Square",
            City = "San Fierro"
        };

        var response = await Client.PostAsJsonAsync("/api/places", request);
        var body = await response.DeserializedBody<Place>();

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        
        body.Should()
            .Match<Place>(x =>
                x.Identification == request.Identification
                && x.City == request.City);
    }

    [Fact]
    public async void WhenItPostsAnEmptyPlace_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/places", new PlacePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors
            .Should()
            .ContainKeys("Identification", "City");
    }

    [Fact]
    public async void WhenItPostsAPlaceWithRepeatedIdentification_ShouldRespondWithIt()
    {
        var request = new PlacePostRequest
        {
            Identification = "Square",
            City = "San Fierro"
        };

        await Client.PostAsJsonAsync("/api/places", request);
        var response = await Client.PostAsJsonAsync("/api/places", request);
        var body = await response.DeserializedBody<ValidationProblemDetails>();
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}
