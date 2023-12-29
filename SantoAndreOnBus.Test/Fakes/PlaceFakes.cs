using SantoAndreOnBus.Api.Domain.Places;

namespace SantoAndreOnBus.Test.ScenarioFakes;

public static class PlaceFakes
{
    public readonly static Place[] Places =
    [
        new() { Identification = "Blue Square", City = "New York" },
        new() { Identification = "Green Street", City = "London" },
        new() { Identification = "Yellow Avenue", City = "Melbourne" }
    ];
}
