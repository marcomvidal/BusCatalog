using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Vehicles;

public class Get(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenvehicleDoesNotExists_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync(
            "/api/vehicles/MEGA_ARTICULATED",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenVehicleExists_ShouldRespondWithIt()
    {
        var vehicle = FakeStore.Vehicles[0];
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Vehicles.AddAsync(vehicle, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var response = await Client.GetAsync(
            $"/api/vehicles/{vehicle.Identification}",
            cancellationToken);
        
        var body = await response.DeserializedBody<Vehicle>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Should()
            .Match<Vehicle>(x => x.Identification == vehicle.Identification);
    }
}
