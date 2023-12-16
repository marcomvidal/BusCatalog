using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SantoAndreOnBus.Api.Business.Places;

public interface IPlaceValidator
{
    Task<ValidationResult> ValidateAsync(
        PlacePostRequest request,
        ModelStateDictionary modelState);

    Task<ValidationResult> ValidateAsync(
        int id,
        PlacePutRequest request,
        ModelStateDictionary modelState);
}

public class PlaceValidator(
    IValidator<PlacePostRequest> postValidator,
    IValidator<PlacePutRequest> putValidator) : IPlaceValidator
{
    private readonly IValidator<PlacePostRequest> _postValidator = postValidator;
    private readonly IValidator<PlacePutRequest> _putValidator = putValidator;

    public async Task<ValidationResult> ValidateAsync(
        PlacePostRequest request,
        ModelStateDictionary modelState)
    {
        var validation = await _postValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }

    public async Task<ValidationResult> ValidateAsync(
        int id,
        PlacePutRequest request,
        ModelStateDictionary modelState)
    {
        request.Id = id;
        var validation = await _putValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }
}