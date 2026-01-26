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
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Lines;

public class Put(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenItUpdatesAValidLine_ShouldRespondWithIt()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var vehicle = FakeStore.Vehicles[0];
        await Context.Vehicles.AddAsync(vehicle, cancellationToken);

        var line = FakeStore.Lines[0] with { Vehicles = [vehicle] };
        await Context.Lines.AddAsync(line, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var request = new LinePutRequest
        {
            Identification = "Valid Identification",
            Fromwards = "Toronto",
            Towards = "Ottawa",
            DeparturesPerDay = 20,
            Vehicles = ["MIDI"]
        };

        var response = await Client.PutAsJsonAsync(
            "/api/lines/1",
            request,
            cancellationToken);

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
    public async Task WhenItUpdatesALineWithNonExistentLinesOrVehicles_ShouldRespondWithValidationErrors()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Lines.AddAsync(FakeStore.Lines[0], cancellationToken);
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Vehicles = ["MIDI", "PADRON"]
        };

        var response = await Client.PutAsJsonAsync(
            "/api/lines/1",
            request,
            cancellationToken);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors
            .Should()
            .ContainKeys("Vehicles");
    }

    [Fact]
    public async Task WhenItPutsALineThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new LinePutRequest
        {
            Identification = "Valid Identification",
            Fromwards = "Toronto",
            Towards = "Ottawa",
            DeparturesPerDay = 20
        };

        var response = await Client.PutAsJsonAsync(
            "/api/lines/0",
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenItPutsAnInvalidLine_ShouldRespondWithValidationErrors()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Lines.AddAsync(FakeStore.Lines[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var response = await Client.PutAsJsonAsync(
            "/api/lines/1",
            new LinePutRequest(),
            cancellationToken);

        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys(
            "Identification", "Fromwards", "Towards", "Vehicles");
    }
}