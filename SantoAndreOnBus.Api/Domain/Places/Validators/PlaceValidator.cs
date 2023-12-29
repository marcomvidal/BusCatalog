using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SantoAndreOnBus.Api.Extensions;

namespace SantoAndreOnBus.Api.Domain.Places;

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
        ModelStateDictionary modelState) =>
        (await _postValidator.ValidateAsync(request))
            .AddToModelState(modelState);

    public async Task<ValidationResult> ValidateAsync(
        int id,
        PlacePutRequest request,
        ModelStateDictionary modelState) =>
        (await _putValidator.ValidateAsync(request with { Id = id }))
            .AddToModelState(modelState);
}