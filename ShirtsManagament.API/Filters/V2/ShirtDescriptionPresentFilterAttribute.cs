using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtsManagament.API.Models;

namespace ShirtsManagament.API.Filters.V2
{
    public class ShirtDescriptionPresentFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            Shirt? shirt = context.ActionArguments["shirt"] as Shirt;
            if (shirt != null && !shirt.ValidateDescription()) 
            {
                context.ModelState.AddModelError("Shirt", "Description is required");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
        }
    }
}
