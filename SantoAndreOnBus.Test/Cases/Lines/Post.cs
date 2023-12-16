using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Lines;

public class Post : IntegrationTest
{
    [Fact]
    public async void WhenItPostsAValidLine_ShouldRespondWithIt()
    {
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            PlacesIdentifiers = [1, 2],
            VehiclesIdentifiers = [1, 2]
        };

        var response = await Client.PostAsJsonAsync("/api/lines", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Line>())
            .Should()
            .Match<Line>(x =>
                x.Identification == request.Identification
                && x.Fromwards == request.Fromwards
                && x.Towards == request.Towards
                && x.DeparturesPerDay == request.DeparturesPerDay);
    }

    [Fact]
    public async void WhenItPostsAnEmptyPlace_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/lines", new LinePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }

    [Fact]
    public async void WhenItPostsAPlaceWithRepeatedIdentification_ShouldRespondWithIt()
    {
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            PlacesIdentifiers = [1, 2],
            VehiclesIdentifiers = [1, 2]
        };

        await Client.PostAsJsonAsync("/api/lines", request);
        var response = await Client.PostAsJsonAsync("/api/lines", request);
        var body = await response.DeserializedBody<ValidationProblemDetails>();
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}
