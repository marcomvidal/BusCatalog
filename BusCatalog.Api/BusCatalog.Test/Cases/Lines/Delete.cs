using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.General;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;
using System.Threading.Tasks;

namespace BusCatalog.Test.Cases.Lines;

public class Delete(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task WhenTheLineExists_ShouldDeleteSuccessfully()
    {
        var id = 1;
        var cancellationToken = TestContext.Current.CancellationToken;
        await Context.Lines.AddAsync(FakeStore.Lines[0], cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        var response = await Client.DeleteAsync($"/api/lines/{id}", cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var body = await response.DeserializedBody<DeleteResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Match<DeleteResponse>(x => x.Data!.Deleted == id);
    }

    [Fact]
    public async Task WhenLineDoesNotExists_ShouldReturnNotFound()
    {
        var response = await Client.DeleteAsync(
            $"/api/lines/0",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
