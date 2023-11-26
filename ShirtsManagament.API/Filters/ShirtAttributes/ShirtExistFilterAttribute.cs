using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtsManagament.API.Models;
using ShirtsManagament.API.Repositories.IRepositories;

namespace ShirtsManagament.API.Filters.ShirtAttributes
{
    public class ShirtExistFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShirtExistFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["id"] as int?;
            Shirt? shirt = await _unitOfWork.ShirtRepo.GetByIdAsync(id);
            if (shirt is null)
            {
                context.ModelState.AddModelError("Id", "Shirt does not exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["shirt"] = shirt;
            await next();
        }
    }
}
