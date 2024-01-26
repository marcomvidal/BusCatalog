using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.General;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.ScenarioFakes;
using Xunit;

namespace BusCatalog.Test.Cases.Lines;

public class Delete(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenTheLineExists_ShouldDeleteSuccessfully()
    {
        var id = 1;
        await Context.Lines.AddAsync(FakeStore.Lines[0]);
        await Context.SaveChangesAsync();

        var response = await Client.DeleteAsync($"/api/lines/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var body = await response.DeserializedBody<DeleteResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<DeleteResponse>(x => x.Data!.Deleted == id);
    }

    [Fact]
    public async void WhenLineDoesNotExists_ShouldReturnNotFound()
    {
        var response = await Client.DeleteAsync($"/api/lines/0");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
