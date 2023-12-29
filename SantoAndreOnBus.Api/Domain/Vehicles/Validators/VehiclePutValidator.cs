using FluentValidation;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

public class VehiclePutValidator : AbstractValidator<VehiclePutRequest>
{
    private readonly IVehicleRepository _repository;

    public VehiclePutValidator(IVehicleRepository repository)
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
        string identification, CancellationToken _) =>
        (await _repository.GetByAsync(x => x.Identification.Equals(identification)))
            .Count == 0;
}
