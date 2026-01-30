using BusCatalog.Api.Domain.Vehicles.Messages;
using BusCatalog.Api.Domain.Vehicles.Ports;
using FluentValidation;

namespace BusCatalog.Api.Domain.Vehicles.Validators;

public sealed class VehiclePutValidator : AbstractValidator<VehiclePutRequest>
{
    private readonly IVehicleRepository _repository;

    public VehiclePutValidator(IVehicleRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotEmpty()
            .Length(4, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage(ValidationMessages.IdentificationShouldBeUnique);

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
