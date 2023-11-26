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
            if (AppRepository.Authenticate(credential.ClientId, credential.Secret)) 
            {
                DateTime expireDate = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    accessToken = CreateToken(credential.ClientId, expireDate),
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

        private string CreateToken(string clientId, DateTime expiresAt)
        {
            Application? application = AppRepository.GetById(clientId);
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim("AppName", application?.Name ?? ""),
                new Claim("Read", (application?.Scopes ?? "").Contains("read") ? "true" : "false"),
                new Claim("Write", (application?.Scopes ?? "").Contains("write") ? "true" : "false"),
            };

            byte[]? secretKey = Encoding.ASCII.GetBytes(_configuration?.GetValue<string>("SecretKey")!);
            JwtSecurityToken jwt = new JwtSecurityToken
            (
            
                signingCredentials: new SigningCredentials 
                (
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                ) ,
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
