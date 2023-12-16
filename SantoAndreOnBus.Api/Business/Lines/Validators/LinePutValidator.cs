using FluentValidation;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LinePutValidator : AbstractValidator<LinePutRequest>
{
    private readonly ILineRepository _repository;

    public LinePutValidator(ILineRepository repository)
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

    private async Task<bool> IdentificationShouldBeUnique(
        LinePutRequest request,
        string identification,
        CancellationToken _)
    {
        var line = await _repository.GetByIdentificationAsync(identification, request.Id);

        return line is null;
    }
}