using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Api.Domain.Lines;

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
    private Line? _line;
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

        return _line!;
    }

    private async Task AddPlaces(IEnumerable<int> places)
    {
        if (_line!.Places.Count > 0)
        {
            _line.Places.Clear();
        }

        _line!.AddPlaces(
            await _placeRepository.GetByAsync(x => places.Contains(x.Id)));
    }

    private async Task AddVehicles(IEnumerable<string> vehicles)
    {
        if (_line!.Vehicles.Count > 0)
        {
            _line.Vehicles.Clear();
        }

        _line!.AddVehicles(
            await _vehicleRepository.GetByAsync(x => vehicles.Contains(x.Identification)));
    }
}
