using FluentAssertions;
using SantoAndreOnBus.Api.Business.Vehicles;
using SantoAndreOnBus.Test.Fakes;
using SantoAndreOnBus.Test.Fixtures;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Vehicles;

public class Put : IntegrationTest
{
    [Fact]
    public async void WhenItUpdatesAValidVehicle_ShouldRespondWithIt()
    {
        await Context.Vehicles.AddAsync(VehicleFakes.Vehicles[0]);
        await Context.SaveChangesAsync();

        var request = new VehicleSubmitRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/vehicles/1", request);
        var body = await response.DeserializedBody<VehicleResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<VehicleResponse>(
            x => x.Data!.Identification == request.NormalizedIdentification);
    }

    [Fact]
    public async void WhenItPutsACompanyThatDoesNotExists_ShouldRespondNotFound()
    {
        var request = new VehicleSubmitRequest
        {
            Identification = "Valid Another Name",
            Description = "Valid Vehicle Changed Name"
        };

        var response = await Client.PutAsJsonAsync("/api/vehicles/0", request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenItPutsAnInvalidCompany_ShouldRespondWithValidationErrors()
    {
        var response = await Client.PutAsJsonAsync("/api/vehicles/1", new VehicleSubmitRequest());
        var body = await response.DeserializedBody<VehicleResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Errors.Should().ContainKeys("Identification");
    }
}