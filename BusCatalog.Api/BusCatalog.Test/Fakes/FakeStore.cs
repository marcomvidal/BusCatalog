using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Test.Fakes;

public static class FakeStore
{
    public static Vehicle[] Vehicles { get => VehiclesFactory.Generate(); }
    public static Line[] Lines { get => LinesFactory.Generate(); }
}
