using System.Linq;
using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.Business.Vehicles;
using SantoAndreOnBus.Test.Fakes;
using SantoAndreOnBus.Test.Fixtures;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Get : IntegrationTest
{
    [Fact]
    public async void WhenItHasNoVehicles_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/vehicles");
        var body = await response.DeserializedBody<VehicleListResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Data.Should().BeEmpty();
    }

    [Fact]
    public async void WhenItHasVehicles_ShouldRespondWithIt()
    {
        var vehicles = new Vehicle[] { VehicleFakes.Vehicles[0], VehicleFakes.Vehicles[1] };
        await Context.Vehicles.AddRangeAsync(vehicles);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync("/api/vehicles");
        var body = await response.DeserializedBody<VehicleListResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Data!
            .Select(x => new { x.Identification, x.Description })
            .Should()
            .BeEquivalentTo(
                vehicles.Select(x => new { x.Identification, x.Description }));
    }
}
