using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.Domain.General;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Delete(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenTheVehicleExists_ShouldDeleteSuccessfully()
    {
        var id = 1;
        await Context.Vehicles.AddAsync(FakeStore.Vehicles()[0]);
        await Context.SaveChangesAsync();

        var response = await Client.DeleteAsync($"/api/vehicles/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var body = await response.DeserializedBody<DeleteResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<DeleteResponse>(x => x.Data!.Deleted == id);
    }

    [Fact]
    public async void WhenTheVehicleDoesNotExists_ShouldReturnNotFound()
    {
        var response = await Client.DeleteAsync($"/api/vehicles/0");
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
