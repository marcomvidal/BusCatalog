using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public interface ILineBuilderService
{
    ILineRelantionshipBuilder WithLine(Line line);
}

public interface ILineRelantionshipBuilder
{
    Task<Line> WithRelantionships(IEnumerable<string> vehicles);
}

public class LineBuilderService(IVehicleRepository vehicleRepository)
    : ILineBuilderService, ILineRelantionshipBuilder
{
    private Line _line = null!;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public ILineRelantionshipBuilder WithLine(Line line)
    {
        _line = line;

        return this;
    }

    public async Task<Line> WithRelantionships(IEnumerable<string> vehicles)
    {
        await AddVehicles(vehicles);

        return _line;
    }

    private async Task AddVehicles(IEnumerable<string> vehiclesIds)
    {
        if (_line.Vehicles.Count > 0)
        {
            _line.Vehicles.Clear();
        }

        var vehicles = await _vehicleRepository.GetByAsync(
            x => vehiclesIds.Contains(x.Identification));
        
        _line.AddVehicles(vehicles);
    }
}
