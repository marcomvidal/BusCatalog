using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Lines;

public class Put : IntegrationTest
{
    [Fact]
    public async void WhenItUpdatesAValidPlace_ShouldRespondWithIt()
    {
        await Context.Lines.AddAsync(FakeStore.Lines[0]);
        await Context.SaveChangesAsync();

        var request = new LinePutRequest
        {
            Identification = "Valid Identification",
            Fromwards = "Toronto",
            Towards = "Ottawa",
            DeparturesPerDay = 20
        };

        var response = await Client.PutAsJsonAsync("/api/lines/1", request);
        var body = await response.DeserializedBody<Line>();

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        body.Should().Match<Line>(
            x => x.Identification == request.Identification);
    }

    [Fact]
    public async void WhenItPutsAPlaceThatDoesNotExists_ShouldRespondNotFound()
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
    public async void WhenItPutsAnInvalidPlace_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PutAsJsonAsync("/api/lines/1", new LinePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}