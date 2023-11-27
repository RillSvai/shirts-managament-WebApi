
using Microsoft.AspNetCore.Mvc;
using ShirtsManagament.API.Authority;
namespace ShirtsManagament.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
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
                DateTime expireDate = DateTime.UtcNow.AddMinutes(60);
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
