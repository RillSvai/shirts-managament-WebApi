using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtsManagament.API.Models;

namespace ShirtsManagament.API.Filters.ShirtAttributes
{
    public class ShirtCreateFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidationProblemDetails details;
            if (context.ActionArguments["shirt"] is not Shirt shirt) 
            {
                context.ModelState.AddModelError("Shirt", "Shirt object is null.");
                details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (shirt.Id != 0) 
            {
                context.ModelState.AddModelError("Id", "Id shouldn`t be specified during creating.");
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
