using FluentValidation;

namespace SantoAndreOnBus.Api.Domain.Places;

public class PlacePutValidator : AbstractValidator<PlacePutRequest>
{
    private readonly IPlaceRepository _repository;

    public PlacePutValidator(IPlaceRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotEmpty()
            .Length(4, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");
        
        RuleFor(x => x.City).NotEmpty().Length(4, 50);
    }

    private async Task<bool> IdentificationShouldBeUnique(
        PlacePutRequest request, string identification, CancellationToken _) =>
        (await _repository.GetByAsync(
            x => x.Identification.Equals(identification) && x.Id != request.Id))
            .Count == 0;
}
