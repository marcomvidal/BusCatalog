using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public interface ILineBuilder
{
    ILineBuilder WithVehicles(IEnumerable<Vehicle> vehiclesIds);
    Line Build();
}

public class LineBuilder : ILineBuilder
{
    private readonly Line _line;

    public LineBuilder(Line line)
    {
        _line = line;
    }

    public ILineBuilder WithVehicles(IEnumerable<Vehicle> vehicles)
    {
        _line.Vehicles.Clear();
        _line.Vehicles.AddRange(vehicles);

        return this;
    }

    public Line Build()
    {
        return _line;
    }
}
