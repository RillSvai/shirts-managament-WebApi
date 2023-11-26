using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShirtsManagament.API.Filters
{
    public class IdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            int? id = context.ActionArguments["id"] as int?;
            if (!(id <= 0) && id is not null) return;
            
            context.ModelState.AddModelError("Id", "Id is invalid.");
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(details);
        }
    }
}
