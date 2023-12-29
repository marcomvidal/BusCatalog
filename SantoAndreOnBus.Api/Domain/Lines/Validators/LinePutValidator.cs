using FluentValidation;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Api.Domain.Lines;

public class LinePutValidator : AbstractValidator<LinePutRequest>
{
    protected readonly ILineRepository _lineRepository;
    protected readonly IVehicleRepository _vehicleRepository;
    protected readonly IPlaceRepository _placeRepository;

    public LinePutValidator(
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

        RuleFor(x => x.Vehicles)
            .NotEmpty()
            .MustAsync(UnknownVehicles)
                .WithMessage("'Vehicles' should refeer to vehicles that exist in database.");

        RuleFor(x => x.Places)
            .NotEmpty()
            .MustAsync(UnknownPlaces)
                .WithMessage("'Places' should refeer to places that exist in database.");
    }

    private async Task<bool> IdentificationShouldBeUnique(
        LinePutRequest request,
        string identification,
        CancellationToken _) =>
        (await _lineRepository.GetByAsync(x =>
            x.Identification == identification && x.Id != request.Id)).Count == 0;

    protected async Task<bool> UnknownVehicles(
        IEnumerable<string> vehicles, CancellationToken _) =>
        (await _vehicleRepository.GetByAsync(x => vehicles.Contains(x.Identification)))
            .Count == vehicles.Count();

    protected async Task<bool> UnknownPlaces(
        IEnumerable<int> places, CancellationToken _) =>
        (await _placeRepository.GetByAsync(x => places.Contains(x.Id)))
            .Count == places.Count();
}
