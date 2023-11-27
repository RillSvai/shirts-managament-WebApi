using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using ShirtsManagament.API.Attributes;
using ShirtsManagament.API.Authority;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ShirtsManagament.API.Filters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            List<Claim>? claims = Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey")!);
            if (claims is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var requiredClaims = context.ActionDescriptor.EndpointMetadata.OfType<RequiredClaimAttribute>().ToList();
            if (requiredClaims is not null && !requiredClaims
                .All(rc => claims
                .Any(c => c.Type.ToLower() == rc.ClaimType.ToLower() && c.Value.ToLower() == rc.ClaimValue.ToLower())))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
