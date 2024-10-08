using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using BusCatalog.Api.Domain.Lines.Ports;

namespace BusCatalog.Test.Cases.Lines;

public class Put(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenItUpdatesAValidLine_ShouldRespondWithIt()
    {
        var vehicle = FakeStore.Vehicles[0];
        await Context.Vehicles.AddAsync(vehicle);

        var line = FakeStore.Lines[0] with { Vehicles = [vehicle] };
        await Context.Lines.AddAsync(line);
        await Context.SaveChangesAsync();

        var request = new LinePutRequest
        {
            Identification = "Valid Identification",
            Fromwards = "Toronto",
            Towards = "Ottawa",
            DeparturesPerDay = 20,
            Vehicles = ["MIDI"]
        };

        var response = await Client.PutAsJsonAsync("/api/lines/1", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Line>())
            .Should()
            .Match<Line>(x =>
                x.Identification == request.Identification
                && x.Fromwards == request.Fromwards
                && x.Towards == request.Towards
                && x.DeparturesPerDay == request.DeparturesPerDay
                && x.Vehicles.Count == request.Vehicles.Count());
    }

    [Fact]
    public async void WhenItUpdatesALineWithNonExistentLinesOrVehicles_ShouldRespondWithValidationErrors()
    {
        await Context.Lines.AddAsync(FakeStore.Lines[0]);
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0]);
        await Context.SaveChangesAsync();
        
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Vehicles = ["MIDI", "PADRON"]
        };

        var response = await Client.PutAsJsonAsync("/api/lines/1", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors.Should().ContainKeys("Vehicles");
    }

    [Fact]
    public async void WhenItPutsALineThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new LinePutRequest
        {
            Identification = "Valid Identification",
            Fromwards = "Toronto",
            Towards = "Ottawa",
            DeparturesPerDay = 20
        };

        var response = await Client.PutAsJsonAsync("/api/lines/0", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenItPutsAnInvalidLine_ShouldRespondWithValidationErrors()
    {
        await Context.Lines.AddAsync(FakeStore.Lines[0]);
        await Context.SaveChangesAsync();

        var response = await Client.PutAsJsonAsync("/api/lines/1", new LinePutRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys(
            "Identification", "Fromwards", "Towards", "Vehicles");
    }
}