using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ShirtsManagament.API.Authority
{
    public static class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {

            Application application = AppRepository.GetById(clientId)!;
            if (application  is null) return false;
            return application.Secret == secret;
        }
        public static string CreateToken(string clientId, DateTime expiresAt, string secretKeyStr)
        {
            Application? application = AppRepository.GetById(clientId);
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim("AppName", application?.Name ?? ""),
                new Claim("Read", (application?.Scopes ?? "").Contains("read") ? "true" : "false"),
                new Claim("Write", (application?.Scopes ?? "").Contains("write") ? "true" : "false"),
            };

            byte[]? secretKey = Encoding.ASCII.GetBytes(secretKeyStr);
            JwtSecurityToken jwt = new JwtSecurityToken
            (

                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static bool VerifyToken(string? token, string secretKeyStr) 
        {
            if (string.IsNullOrEmpty(token?.Trim())) 
            {
                return false;
            }
            byte[] secretKey = Encoding.ASCII.GetBytes(secretKeyStr);
            SecurityToken securityToken;
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out securityToken);
            }
            catch(SecurityTokenException) 
            {
                return false;
            }
            catch
            {
                throw;
            }
            return securityToken is not null ;
        }
    }
}
