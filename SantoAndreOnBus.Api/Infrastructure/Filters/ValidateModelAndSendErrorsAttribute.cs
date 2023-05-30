using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SantoAndreOnBus.Api.Infrastructure.Filters;

public class ValidateModelAndSendErrorsAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
            context.Result = new BadRequestObjectResult(
                context.ModelState.Values.SelectMany(e => e.Errors));
    }
}
