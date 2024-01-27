using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Test.Fixtures;
using BusCatalog.Test.Fakes;
using Xunit;

namespace BusCatalog.Test.Cases.Places;

public class GetAll(TestWebApplicationFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async void WhenItHasNoPlaces_ShouldRespondEmpty()
    {
        var response = await Client.GetAsync("/api/places");
        var body = await response.DeserializedBody<IEnumerable<Place>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Should().BeEmpty();
    }

    [Fact]
    public async void WhenItHasPlaces_ShouldRespondWithIt()
    {
        var places = FakeStore.Places.Take(2);
        await Context.Places.AddRangeAsync(places);
        await Context.SaveChangesAsync();
        
        var response = await Client.GetAsync("/api/places");
        var body = await response.DeserializedBody<IEnumerable<Place>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        body!
            .Select(x => new { x.Identification, x.City })
            .Should()
            .BeEquivalentTo(
                places.Select(x => new { x.Identification, x.City }));
    }
}
