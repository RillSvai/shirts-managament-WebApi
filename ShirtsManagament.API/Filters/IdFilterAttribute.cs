using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShirtsManagament.API.Filters
{
    public class IdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ValidationProblemDetails details;
            int? id = context.ActionArguments["id"] as int?;
            if (id <= 0 || id is null)
            {
                context.ModelState.AddModelError("Id", "Id is invalid.");
                details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
            }
        }
    }
}
