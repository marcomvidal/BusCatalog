using FluentValidation;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LinePostValidator : AbstractValidator<LinePostRequest>
{
    private readonly ILineRepository _repository;

    public LinePostValidator(ILineRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Identification)
            .NotNull()
            .Length(2, 50)
            .MustAsync(IdentificationShouldBeUnique)
                .WithMessage("'Identification' should be unique.");
        
        RuleFor(x => x.Fromwards).NotNull().Length(3, 50);
        RuleFor(x => x.Towards).NotNull().Length(3, 50);
        RuleFor(x => x.DeparturesPerDay).NotNull();
    }

    private async Task<bool> IdentificationShouldBeUnique(string identification, CancellationToken _)
    {
        var line = await _repository.GetByIdentificationAsync(identification);

        return line is null;
    }
}