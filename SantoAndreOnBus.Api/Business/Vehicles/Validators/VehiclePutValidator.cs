using FluentValidation;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public class VehiclePutValidator : AbstractValidator<VehiclePostRequest>
{
    private readonly IVehicleRepository _repository;

    public VehiclePutValidator(IVehicleRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotNull()
            .Length(4, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");

        RuleFor(x => x.Description).NotNull().Length(4, 50);
    }

    private async Task<bool> IdentificationShouldBeUnique(string identification, CancellationToken _)
    {
        var vehicle = await _repository.GetByIdentificationAsync(identification);

        return vehicle is null;
    }
}