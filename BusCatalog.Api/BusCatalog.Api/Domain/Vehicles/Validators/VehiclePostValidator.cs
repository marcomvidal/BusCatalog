using BusCatalog.Api.Domain.Vehicles.Ports;
using FluentValidation;

namespace BusCatalog.Api.Domain.Vehicles.Validators;

public class VehiclePostValidator : AbstractValidator<VehiclePostRequest>
{
    private readonly IVehicleRepository _repository;

    public VehiclePostValidator(IVehicleRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotEmpty()
            .Length(4, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");

        RuleFor(x => x.Description).NotEmpty().Length(4, 50);
    }

    private async Task<bool> IdentificationShouldBeUnique(
        string identification,
        CancellationToken _)
    {
        var vehicles = await _repository.GetByAsync(x => x.Identification.Equals(identification));

        return vehicles.Count == 0;
    }
}