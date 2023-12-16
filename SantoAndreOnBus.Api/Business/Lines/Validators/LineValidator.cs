using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SantoAndreOnBus.Api.Business.Lines;

public interface ILineValidator
{
    Task<ValidationResult> ValidateAsync(
        LinePostRequest request,
        ModelStateDictionary modelState);

    Task<ValidationResult> ValidateAsync(
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
        ModelStateDictionary modelState)
    {
        var validation = await _postValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }

    public async Task<ValidationResult> ValidateAsync(
        LinePutRequest request,
        ModelStateDictionary modelState)
    {
        var validation = await _putValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }
}
