using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Test.ScenarioFakes;

public static class FakeStore
{
    public static readonly Vehicle[] Vehicles = VehicleFakes.Vehicles;
    public static readonly Place[] Places = PlaceFakes.Places;
    public static readonly Line[] Lines = LineFakes.Lines;
}
