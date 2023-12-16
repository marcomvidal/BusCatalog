using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Places;

public class Put : IntegrationTest
{
    [Fact]
    public async void WhenItUpdatesAValidPlace_ShouldRespondWithIt()
    {
        await Context.Places.AddAsync(FakeStore.Places[0]);
        await Context.SaveChangesAsync();

        var request = new PlacePutRequest
        {
            Identification = "Valid Identification",
            City = "Valid City Name"
        };

        var response = await Client.PutAsJsonAsync("/api/places/1", request);
        var body = await response.DeserializedBody<Place>();

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        body.Should().Match<Place>(
            x => x.Identification == request.Identification);
    }

    [Fact]
    public async void WhenItPutsAPlaceThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new PlacePutRequest
        {
            Identification = "Valid Another Name",
            City = "Valid City Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/places/0", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenItPutsAnInvalidPlace_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PutAsJsonAsync("/api/places/1", new PlacePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}