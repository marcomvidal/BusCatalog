using FluentValidation;
using BusCatalog.Api.Domain.Vehicles;
using BusCatalog.Api.Domain.Lines.Ports;

namespace BusCatalog.Api.Domain.Lines.Validators;

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
        CancellationToken _)
    {
        var lines = await _lineRepository.GetByAsync(
            x => x.Identification == identification && x.Id != request.Id);
            
        return lines.Count == 0;
    }
        

    protected async Task<bool> UnknownVehicles(
        IEnumerable<string> requestVehicles,
        CancellationToken _)
    {
        var vehicles = await _vehicleRepository.GetByAsync(x => requestVehicles.Contains(x.Identification));

        return requestVehicles.Count() == vehicles.Count;
    }
}
