using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public interface ILineBuilderService
{
    ILineRelantionshipBuilder WithLine(Line line);
}

public interface ILineRelantionshipBuilder
{
    Task<Line> WithRelantionships(IEnumerable<int> places, IEnumerable<string> vehicles);
}

public class LineBuilderService(
    IPlaceRepository placeRepository,
    IVehicleRepository vehicleRepository) : ILineBuilderService, ILineRelantionshipBuilder
{
    private Line _line = null!;
    private readonly IPlaceRepository _placeRepository = placeRepository;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public ILineRelantionshipBuilder WithLine(Line line)
    {
        _line = line;

        return this;
    }

    public async Task<Line> WithRelantionships(
        IEnumerable<int> places,
        IEnumerable<string> vehicles)
    {
        await AddPlaces(places);
        await AddVehicles(vehicles);

        return _line;
    }

    private async Task AddPlaces(IEnumerable<int> placesIds)
    {
        if (_line.Places.Count > 0)
        {
            _line.Places.Clear();
        }

        var places = await _placeRepository.GetByAsync(x => placesIds.Contains(x.Id));
        _line.AddPlaces(places);
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
