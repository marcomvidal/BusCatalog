using FluentValidation;

namespace SantoAndreOnBus.Api.Business.Places;

public class PlacePostValidator : AbstractValidator<PlacePostRequest>
{
    private readonly IPlaceRepository _repository;

    public PlacePostValidator(IPlaceRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotNull()
            .Length(4, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");
        
        RuleFor(x => x.City).NotNull().Length(4, 50);
    }

    private async Task<bool> IdentificationShouldBeUnique(string identification, CancellationToken _)
    {
        var place = await _repository.GetByIdentificationAsync(identification);

        return place is null;
    }
}
