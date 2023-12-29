using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SantoAndreOnBus.Api.Extensions;

public static class IValidatorExtensions
{
    public static ValidationResult AddToModelState(
        this ValidationResult validation,
        ModelStateDictionary modelState)
    {
        foreach (var error in validation.Errors) 
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return validation;
    }
}