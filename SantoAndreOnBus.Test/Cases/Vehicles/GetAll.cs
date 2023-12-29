using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.Domain.Vehicles;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class GetAll : IntegrationTest
{
    [Fact]
    public async void WhenItHasNoVehicles_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/vehicles");
        var body = await response.DeserializedBody<IEnumerable<Vehicle>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEmpty();
    }

    [Fact]
    public async void WhenItHasVehicles_ShouldRespondWithIt()
    {
        var vehicles = FakeStore.Vehicles[0..1];
        await Context.Vehicles.AddRangeAsync(vehicles);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync("/api/vehicles");
        var body = await response.DeserializedBody<IEnumerable<Vehicle>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Select(x => new { x.Identification, x.Description })
            .Should()
            .BeEquivalentTo(
                vehicles.Select(x => new { x.Identification, x.Description }));
    }
}
