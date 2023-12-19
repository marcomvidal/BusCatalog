using FluentValidation;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LinePostValidator : AbstractValidator<LinePostRequest>
{
    private readonly ILineRepository _lineRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IPlaceRepository _placeRepository;

    public LinePostValidator(
        ILineRepository lineRepository,
        IVehicleRepository vehicleRepository,
        IPlaceRepository placeRepository)
    {
        _lineRepository = lineRepository;
        _vehicleRepository = vehicleRepository;
        _placeRepository = placeRepository;

        RuleFor(x => x.Identification)
            .NotNull()
            .Length(2, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");
        
        RuleFor(x => x.Fromwards).NotNull().Length(3, 50);
        RuleFor(x => x.Towards).NotNull().Length(3, 50);
        RuleFor(x => x.DeparturesPerDay).NotNull();

        RuleFor(x => x.VehiclesIdentifications)
            .NotNull()
            .NotEmpty()
            .MustAsync(VehiclesExist)
                .WithMessage("'VehiclesIdentifications' should refeer to vehicles that exists in database.");

        RuleFor(x => x.PlacesIdList)
            .NotNull()
            .NotEmpty()
            .MustAsync(PlacesExist)
                .WithMessage("'PlacesIdList' should refeer to places that exists in database.");
    }

    private async Task<bool> IdentificationShouldBeUnique(
        string identification,
        CancellationToken _)
    {
        var line = await _lineRepository.GetByIdentificationAsync(identification);

        return line is null;
    }

    private async Task<bool> VehiclesExist(
        IEnumerable<string> vehicleIdentifiers,
        CancellationToken _)
    {
        var vehicles = await _vehicleRepository.GetByIdentificationAsync(vehicleIdentifiers);

        return vehicles.Any();
    }

    private async Task<bool> PlacesExist(IEnumerable<int> placesIdList, CancellationToken _)
    {
        var places = await _placeRepository.GetByIdAsync(placesIdList);

        return places.Any();
    }
}