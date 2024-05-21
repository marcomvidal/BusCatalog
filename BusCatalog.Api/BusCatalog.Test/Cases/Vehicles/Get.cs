using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;

namespace BusCatalog.Test.Cases.Vehicles;

public class Get(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenvehicleDoesNotExists_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/vehicles/MEGA_ARTICULATED");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenVehicleExists_ShouldRespondWithIt()
    {
        var vehicle = FakeStore.Vehicles[0];
        await Context.Vehicles.AddAsync(vehicle);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync($"/api/vehicles/{vehicle.Identification}");
        var body = await response.DeserializedBody<Vehicle>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Should().Match<Vehicle>(x => x.Identification == vehicle.Identification);
    }
}
