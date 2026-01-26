using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using BusCatalog.Api.Domain.Lines.Ports;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Lines;

public class Post(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenItPostsAValidLine_ShouldRespondWithIt()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Vehicles.AddRangeAsync(FakeStore.Vehicles.Take(2), cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        
        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Vehicles = ["MIDI", "PADRON"]
        };

        var response = await Client.PostAsJsonAsync(
            "/api/lines",
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
    public async Task WhenItPostsALineWithNonExistentLinesOrVehicles_ShouldRespondWithValidationErrors()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
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

        var response = await Client.PostAsJsonAsync(
            "/api/lines",
            request,
            cancellationToken);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors
            .Should()
            .ContainKeys("Vehicles");
    }

    [Fact]
    public async Task WhenItPostsAnEmptyLine_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PostAsJsonAsync(
            "/api/lines",
            new LinePostRequest(),
            TestContext.Current.CancellationToken);
        
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys(
            "Identification", "Fromwards", "Towards", "Vehicles");
    }

    [Fact]
    public async Task WhenItPostsALineWithRepeatedIdentification_ShouldRespondWithIt()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var request = new LinePostRequest
        {
            Identification = "101",
            Fromwards = "California",
            Towards = "Alaska",
            DeparturesPerDay = 10,
            Vehicles = ["MIDI"]
        };

        await Client.PostAsJsonAsync(
            "/api/lines",
            request,
            cancellationToken);
        
        var response = await Client.PostAsJsonAsync(
            "/api/lines",
            request,
            cancellationToken);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors
            .Should()
            .ContainKeys("Identification");
    }
}
