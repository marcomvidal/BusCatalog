using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SantoAndreOnBus.Api.Extensions;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

public interface IVehicleValidator
{
    Task<ValidationResult> ValidateAsync(
        VehiclePostRequest request,
        ModelStateDictionary modelState);

    Task<ValidationResult> ValidateAsync(
        int id,
        VehiclePutRequest request,
        ModelStateDictionary modelState);
}

public class VehicleValidator(
    IValidator<VehiclePostRequest> postValidator,
    IValidator<VehiclePutRequest> putValidator) : IVehicleValidator
{
    private readonly IValidator<VehiclePostRequest> _postValidator = postValidator;
    private readonly IValidator<VehiclePutRequest> _putValidator = putValidator;

    public async Task<ValidationResult> ValidateAsync(
        VehiclePostRequest request,
        ModelStateDictionary modelState) =>
        (await _postValidator.ValidateAsync(request))
            .AddToModelState(modelState);

    public async Task<ValidationResult> ValidateAsync(
        int id,
        VehiclePutRequest request,
        ModelStateDictionary modelState) =>
        (await _putValidator.ValidateAsync(request with { Id = id }))
            .AddToModelState(modelState);
}
