using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Places;

public class Post : IntegrationTest
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

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Place>())
            .Should()
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
        body!.Errors.Should().ContainKeys("Identification");
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
