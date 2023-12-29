using SantoAndreOnBus.Api.Domain.Lines;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Test.ScenarioFakes;

public static class FakeStore
{
    public static readonly Vehicle[] Vehicles = VehicleFakes.Vehicles;
    public static readonly Place[] Places = PlaceFakes.Places;
    public static readonly Line[] Lines = LineFakes.Lines;
}
