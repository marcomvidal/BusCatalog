using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Lines;

public class GetAll(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenItHasNoPlaces_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync(
            "/api/lines",
            TestContext.Current.CancellationToken);

        var body = await response.DeserializedBody<IEnumerable<Line>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEmpty();
    }

    [Fact]
    public async Task WhenItHasLines_ShouldRespondWithIt()
    {
        var lines = FakeStore.Lines.Take(2);
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Lines.AddRangeAsync(lines, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var response = await Client.GetAsync("/api/lines", cancellationToken);
        var body = await response.DeserializedBody<IEnumerable<Line>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!
            .Select(x => new { x.Identification, x.Fromwards, x.Towards })
            .Should()
            .BeEquivalentTo(
                lines.Select(x => new { x.Identification, x.Fromwards, x.Towards }));
    }
}
