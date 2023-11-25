using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtsManagament.API.Models;

namespace ShirtsManagament.API.Filters.ShirtAttributes
{
    public class ShirtUpdateFilterAttribute : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["id"] as int?;
            Shirt? shirt = context.ActionArguments["shirt"] as Shirt;
            ValidationProblemDetails details;
            if (id == null || shirt is null || shirt.Id != id) 
            {
                context.ModelState.AddModelError("Shirt", "Id from route and id of updated shirt should match.");
                details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            await next();
        }
    }
}
