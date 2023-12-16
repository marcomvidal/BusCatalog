using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Test.Fixtures;
using SantoAndreOnBus.Test.ScenarioFakes;
using Xunit;

namespace SantoAndreOnBus.Test.Cases.Lines;

public class GetAll : IntegrationTest
{
    [Fact]
    public async void WhenItHasNoPlaces_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/lines");
        var body = await response.DeserializedBody<IEnumerable<Line>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEmpty();
    }

    [Fact]
    public async void WhenItHasLines_ShouldRespondWithIt()
    {
        var lines = FakeStore.Lines[0..1];
        await Context.Lines.AddRangeAsync(lines);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync("/api/lines");
        var body = await response.DeserializedBody<IEnumerable<Line>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Select(x => new { x.Identification, x.Fromwards, x.Towards })
            .Should()
            .BeEquivalentTo(
                lines.Select(x => new { x.Identification, x.Fromwards, x.Towards }));
    }
}