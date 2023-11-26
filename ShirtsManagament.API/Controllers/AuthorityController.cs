using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShirtsManagament.API.Authority;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
namespace ShirtsManagament.API.Controllers
{
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthorityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] ApplicationCredential credential) 
        {
            if (Authenticator.Authenticate(credential.ClientId, credential.Secret)) 
            {
                DateTime expireDate = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    accessToken = Authenticator.CreateToken(credential.ClientId, expireDate, _configuration.GetValue<string>("SecretKey")!),
                    expiresAt = expireDate
                });
            }
            ModelState.AddModelError("Unauthorized", "You are not authorized.");
            ValidationProblemDetails details = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status401Unauthorized
            };
            return new UnauthorizedObjectResult(details);
        }  
    }
}
