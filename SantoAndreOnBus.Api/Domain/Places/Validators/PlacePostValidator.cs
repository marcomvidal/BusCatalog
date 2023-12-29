using FluentValidation;

namespace SantoAndreOnBus.Api.Domain.Places;

public class PlacePostValidator : AbstractValidator<PlacePostRequest>
{
    private readonly IPlaceRepository _repository;

    public PlacePostValidator(IPlaceRepository repository)
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
        string identification, CancellationToken _) =>
        (await _repository.GetByAsync(x => x.Identification.Contains(identification)))
            .Count == 0;
}
