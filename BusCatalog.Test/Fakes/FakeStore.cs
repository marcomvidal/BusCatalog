using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Test.ScenarioFakes;

public static class FakeStore
{
    public static Vehicle[] Vehicles() => VehicleFakes.Vehicles();
    public static Place[] Places() => PlaceFakes.Places();
    public static Line[] Lines() => LineFakes.Lines();
}
