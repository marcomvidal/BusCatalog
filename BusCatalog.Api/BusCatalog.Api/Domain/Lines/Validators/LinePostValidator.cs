using FluentValidation;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public class LinePostValidator : AbstractValidator<LinePostRequest>
{
    protected readonly ILineRepository _lineRepository;
    protected readonly IVehicleRepository _vehicleRepository;
    protected readonly IPlaceRepository _placeRepository;

    public LinePostValidator(
        ILineRepository lineRepository,
        IVehicleRepository vehicleRepository,
        IPlaceRepository placeRepository)
    {
        _lineRepository = lineRepository;
        _vehicleRepository = vehicleRepository;
        _placeRepository = placeRepository;

        RuleFor(x => x.Identification)
            .NotEmpty()
            .Length(2, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");
        
        RuleFor(x => x.Fromwards).NotEmpty().Length(3, 50);
        RuleFor(x => x.Towards).NotEmpty().Length(3, 50);
        RuleFor(x => x.DeparturesPerDay).NotNull();

        RuleFor(x => x.Vehicles)
            .NotEmpty()
            .MustAsync(VehiclesDoesNotExists)
                .WithMessage("'Vehicles' should refeer to vehicles that exist in database.");

        RuleFor(x => x.Places)
            .NotEmpty()
            .MustAsync(PlacesDoesNotExists)
                .WithMessage("'Places' should refeer to places that exist in database.");
    }

    protected async Task<bool> IdentificationShouldBeUnique(
        string identification, CancellationToken _) =>
            (await _lineRepository.GetByAsync(x => x.Identification == identification))
            .Count == 0;
        

    protected async Task<bool> VehiclesDoesNotExists(
        IEnumerable<string> vehicles, CancellationToken _) =>
        (await _vehicleRepository.GetByAsync(x => vehicles.Contains(x.Identification)))
            .Count == vehicles.Count();

    protected async Task<bool> PlacesDoesNotExists(
        IEnumerable<int> places, CancellationToken _) =>
        (await _placeRepository.GetByAsync(x => places.Contains(x.Id)))
            .Count == places.Count();
}