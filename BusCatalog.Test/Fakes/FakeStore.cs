using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Test.ScenarioFakes;

public static class FakeStore
{
    public static Vehicle[] Vehicles { get => VehicleFakes.Vehicles(); }
    public static Place[] Places { get => PlaceFakes.Places(); }
    public static Line[] Lines { get => LineFakes.Lines(); }
}
