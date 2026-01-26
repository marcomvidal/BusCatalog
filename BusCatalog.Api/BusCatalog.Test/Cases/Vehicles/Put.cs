using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using BusCatalog.Api.Domain.Vehicles.Ports;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Vehicles;

public class Put(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenItUpdatesAValidVehicle_ShouldRespondWithIt()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var request = new VehiclePutRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync(
            "/api/vehicles/1",
            request,
            cancellationToken);
        
        var body = await response.DeserializedBody<Vehicle>();

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        
        body.Should().Match<Vehicle>(
            x => x.Identification == request.Identification.UpperSnakeCasefy());
    }

    [Fact]
    public async Task WhenItPutsAVehicleThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new VehiclePutRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync(
            "/api/vehicles/0",
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenItPutsAnInvalidVehicle_ShouldRespondWithValidationErrors()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Vehicles.AddAsync(FakeStore.Vehicles[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        
        var response = await Client.PutAsJsonAsync(
            "/api/vehicles/1",
            new VehiclePutRequest { Identification = null!, Description = null! },
            cancellationToken);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.DeserializedBody<ValidationProblemDetails>())!
            .Errors
            .Should()
            .ContainKeys("Identification", "Description");
    }
}