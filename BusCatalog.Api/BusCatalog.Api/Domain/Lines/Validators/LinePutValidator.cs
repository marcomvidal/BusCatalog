using FluentValidation;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public class LinePutValidator : AbstractValidator<LinePutRequest>
{
    protected readonly ILineRepository _lineRepository;
    protected readonly IVehicleRepository _vehicleRepository;

    public LinePutValidator(
        ILineRepository lineRepository,
        IVehicleRepository vehicleRepository)
    {
        _lineRepository = lineRepository;
        _vehicleRepository = vehicleRepository;

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
}
