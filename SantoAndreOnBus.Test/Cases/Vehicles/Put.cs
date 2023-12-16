using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Business.Vehicles;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Put : IntegrationTest
{
    [Fact]
    public async void WhenItUpdatesAValidVehicle_ShouldRespondWithIt()
    {
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0]);
        await Context.SaveChangesAsync();

        var request = new VehiclePostRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/vehicles/1", request);
        var body = await response.DeserializedBody<Vehicle>();

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        body.Should().Match<Vehicle>(
            x => x.Identification == request.NormalizedIdentification);
    }

    [Fact]
    public async void WhenItPutsAVehicleThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new VehiclePostRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/vehicles/0", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenItPutsAnInvalidVehicle_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PutAsJsonAsync("/api/vehicles/1", new VehiclePostRequest());
        var body = await response.DeserializedBody<ValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}