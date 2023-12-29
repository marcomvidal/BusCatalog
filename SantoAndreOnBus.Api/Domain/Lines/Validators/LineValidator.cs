using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SantoAndreOnBus.Api.Extensions;

namespace SantoAndreOnBus.Api.Domain.Lines;

public interface ILineValidator
{
    Task<ValidationResult> ValidateAsync(
        LinePostRequest request,
        ModelStateDictionary modelState);

    Task<ValidationResult> ValidateAsync(
        int id,
        LinePutRequest request,
        ModelStateDictionary modelState);
}

public class LineValidator(
    IValidator<LinePostRequest> postValidator,
    IValidator<LinePutRequest> putValidator) : ILineValidator
{
    private readonly IValidator<LinePostRequest> _postValidator = postValidator;
    private readonly IValidator<LinePutRequest> _putValidator = putValidator;

    public async Task<ValidationResult> ValidateAsync(
        LinePostRequest request,
        ModelStateDictionary modelState) =>
        (await _postValidator.ValidateAsync(request))
            .AddToModelState(modelState);

    public async Task<ValidationResult> ValidateAsync(
        int id,
        LinePutRequest request,
        ModelStateDictionary modelState) =>
        (await _putValidator.ValidateAsync(request with { Id = id }))
            .AddToModelState(modelState);
}
