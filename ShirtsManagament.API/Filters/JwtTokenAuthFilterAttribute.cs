using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using ShirtsManagament.API.Authority;

namespace ShirtsManagament.API.Filters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue("Authorization",out StringValues token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            if (!Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey")!)) 
            {
                context.Result = new UnauthorizedResult();  
            }

        }
    }
}
