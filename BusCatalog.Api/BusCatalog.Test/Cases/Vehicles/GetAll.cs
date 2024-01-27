using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;

namespace BusCatalog.Test.Cases.Vehicles;

public class GetAll(TestWebApplicationFactory factory) : IntegrationTest(factory)
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
        var vehicles = FakeStore.Vehicles.Take(2);
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
