using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusCatalog.Api.Extensions;

public static class IValidatorExtensions
{
    public static async Task<ValidationResult> ValidateModelAsync<T>(
        this IValidator<T> validator,
        T instance,
        ModelStateDictionary modelState)
    {
        var validation = await validator.ValidateAsync(instance);

        foreach (var error in validation.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validation;
    }
}