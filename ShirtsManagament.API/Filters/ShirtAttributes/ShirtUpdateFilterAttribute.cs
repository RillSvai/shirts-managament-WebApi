using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtsManagament.API.Models;

namespace ShirtsManagament.API.Filters.ShirtAttributes
{
    public class ShirtUpdateFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is not int id || context.ActionArguments["shirt"] is not Shirt shirt || shirt.Id != (int?)id) 
            {
                context.ModelState.AddModelError("Shirt", "Id from route and id of updated shirt should match.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
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
