using BusCatalog.Api.Domain.Places;

namespace BusCatalog.Test.Fakes;

public class PlacesFactory : IFakeFactory<Place>
{
    public static Place[] Generate() =>
        [
            new() { Id = 1, Identification = "Blue Square", City = "New York" },
            new() { Id = 2, Identification = "Green Street", City = "London" },
            new() { Id = 3, Identification = "Yellow Avenue", City = "Melbourne" }
        ];
}
