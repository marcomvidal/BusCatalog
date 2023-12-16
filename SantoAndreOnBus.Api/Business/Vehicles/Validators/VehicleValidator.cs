using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SantoAndreOnBus.Api.Business.Vehicles;

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
        ModelStateDictionary modelState)
    {
        var validation = await _postValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }

    public async Task<ValidationResult> ValidateAsync(
        int id,
        VehiclePutRequest request,
        ModelStateDictionary modelState)
    {
        request.Id = id;
        var validation = await _putValidator.ValidateAsync(request);
        validation.AddToModelState(modelState);

        return validation;
    }
}
