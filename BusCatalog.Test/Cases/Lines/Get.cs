using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.ScenarioFakes;
using Xunit;

namespace BusCatalog.Test.Cases.Lines;

public class Get(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenLineDoesNotExists_ShouldRespondNotFound()
    {
        var response = await Client.GetAsync("/api/lines/1");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void WhenLineExists_ShouldRespondWithIt()
    {
        var line = FakeStore.Lines[0];
        await Context.Lines.AddAsync(line);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync($"/api/lines/{line.Identification}");
        var body = await response.DeserializedBody<Line>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Should()
            .BeEquivalentTo(line);
    }
}
