using SantoAndreOnBus.Api.Domain.Lines;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Test.ScenarioFakes;

public static class FakeStore
{
    public static Vehicle[] Vehicles() => VehicleFakes.Vehicles();
    public static Place[] Places() => PlaceFakes.Places();
    public static Line[] Lines() => LineFakes.Lines();
}
