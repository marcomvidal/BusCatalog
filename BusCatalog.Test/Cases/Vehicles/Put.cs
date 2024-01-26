using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.ScenarioFakes;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace BusCatalog.Test.Cases.Vehicles;

public class Put(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenItUpdatesAValidVehicle_ShouldRespondWithIt()
    {
        await Context.Vehicles.AddAsync(FakeStore.Vehicles()[0]);
        await Context.SaveChangesAsync();

        var request = new VehiclePutRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/vehicles/1", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        (await response.DeserializedBody<Vehicle>())
            .Should().Match<Vehicle>(
            x => x.Identification == request.Identification);
    }

    [Fact]
    public async void WhenItPutsAVehicleThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new VehiclePutRequest
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
        await Context.Vehicles.AddAsync(FakeStore.Vehicles()[0] with { Id = 1 });
        await Context.SaveChangesAsync();
        
        var response = await Client.PutAsJsonAsync("/api/vehicles/1", new VehiclePutRequest());
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors.Should().ContainKeys("Identification", "Description");
    }
}