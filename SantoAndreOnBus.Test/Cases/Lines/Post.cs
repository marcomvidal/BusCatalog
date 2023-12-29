using System.Linq;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Domain.Lines;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Lines;

public class Post : IntegrationTest
{
    [Fact]
    public async void WhenItPostsAValidLine_ShouldRespondWithIt()
    {
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0]);
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[1]);
        await Context.Places.AddAsync(FakeStore.Places[0]);
        await Context.Places.AddAsync(FakeStore.Places[1]);
        await Context.SaveChangesAsync();
        
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Places = [1, 2],
            Vehicles = ["MIDI", "PADRON"]
        };

        var response = await Client.PostAsJsonAsync("/api/lines", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Line>())
            .Should()
            .Match<Line>(x =>
                x.Identification == request.Identification
                && x.Fromwards == request.Fromwards
                && x.Towards == request.Towards
                && x.DeparturesPerDay == request.DeparturesPerDay
                && x.Places.Count == request.Places.Count()
                && x.Vehicles.Count == request.Vehicles.Count());
    }

    [Fact]
    public async void WhenItPostsALineWithNonExistentLinesOrVehicles_ShouldRespondWithValidationErrors()
    {
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0]);
        await Context.Places.AddAsync(FakeStore.Places[0]);
        await Context.SaveChangesAsync();
        
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Places = [1, 2],
            Vehicles = ["MIDI", "PADRON"]
        };

        var response = await Client.PostAsJsonAsync("/api/lines", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors.Should().ContainKeys("Places", "Vehicles");
    }

    [Fact]
    public async void WhenItPostsAnEmptyLine_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync("/api/lines", new LinePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys(
            "Identification", "Fromwards", "Towards", "Vehicles", "Places");
    }

    [Fact]
    public async void WhenItPostsALineWithRepeatedIdentification_ShouldRespondWithIt()
    {
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Places = [1, 2],
            Vehicles = ["ARTICULADO", "PADRON"]
        };

        await Client.PostAsJsonAsync("/api/lines", request);
        var response = await Client.PostAsJsonAsync("/api/lines", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors.Should().ContainKeys("Identification");
    }
}
