using BusCatalog.Api.Domain.Places;

namespace BusCatalog.Test.ScenarioFakes;

public static class PlaceFakes
{
    public static Place[] Places()
    {
        return [
            new() { Id = 1, Identification = "Blue Square", City = "New York" },
            new() { Id = 2, Identification = "Green Street", City = "London" },
            new() { Id = 3, Identification = "Yellow Avenue", City = "Melbourne" }
        ];
    }
}
