using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Lines;

public class Get(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenLineDoesNotExists_ShouldRespondNotFound()
    {
        var response = await Client.GetAsync(
            "/api/lines/1",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenLineExists_ShouldRespondWithIt()
    {
        var line = FakeStore.Lines[0];
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Lines.AddAsync(line, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var response = await Client.GetAsync(
            $"/api/lines/{line.Identification}",
            cancellationToken);
        
        var body = await response.DeserializedBody<Line>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEquivalentTo(line);
    }
}
