using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Vehicles;

public class GetAll(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenItHasNoVehicles_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync(
            "/api/vehicles",
            TestContext.Current.CancellationToken);
        
        var body = await response.DeserializedBody<IEnumerable<Vehicle>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEmpty();
    }

    [Fact]
    public async Task WhenItHasVehicles_ShouldRespondWithIt()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var vehicles = FakeStore.Vehicles.Take(2);
        await Context.Vehicles.AddRangeAsync(vehicles, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        
        var response = await Client.GetAsync("/api/vehicles", cancellationToken);
        var body = await response.DeserializedBody<IEnumerable<Vehicle>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Select(x => new { x.Identification, x.Description })
            .Should()
            .BeEquivalentTo(
                vehicles.Select(x => new { x.Identification, x.Description }));
    }
}
