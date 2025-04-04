using FluentValidation;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Domain.Lines.Messages;

namespace BusCatalog.Api.Domain.Lines.Validators;

public class LinePostValidator : AbstractValidator<LinePostRequest>
{
    protected readonly ILineRepository _lineRepository;
    protected readonly IVehicleRepository _vehicleRepository;

    public LinePostValidator(
        ILineRepository lineRepository,
        IVehicleRepository vehicleRepository)
    {
        _lineRepository = lineRepository;
        _vehicleRepository = vehicleRepository;

        RuleFor(x => x.Identification)
            .NotEmpty()
            .Length(2, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage(ValidationMessages.IdentificationShouldBeUnique);
        
        RuleFor(x => x.Fromwards).NotEmpty().Length(3, 50);
        RuleFor(x => x.Towards).NotEmpty().Length(3, 50);
        RuleFor(x => x.DeparturesPerDay).GreaterThan(0);

        RuleFor(x => x.Vehicles)
            .NotEmpty()
            .MustAsync(VehiclesDoesNotExists)
                .WithMessage(ValidationMessages.VehiclesDoesNotExists);
    }

    protected async Task<bool> IdentificationShouldBeUnique(
        string identification,
        CancellationToken _)
    {
        var lines = await _lineRepository.GetByAsync(x => x.Identification == identification);
        
        return lines.Count == 0;
    }
    
    protected async Task<bool> VehiclesDoesNotExists(
        IEnumerable<string> requestVehicles,
        CancellationToken _)
    {
        var vehicles = await _vehicleRepository.GetByAsync(
            x => requestVehicles.Contains(x.Identification));
        
        return requestVehicles.Count() == vehicles.Count;
    }
}