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
            List<Claim> claims = new List<Claim>()
            {
                new Claim("AppName", application?.Name ?? ""),
            };

            string[]? scopes = application.Scopes?.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (string scope in scopes ?? new string[0]) 
            {
                claims.Add(new Claim(scope.ToLower(), "true"));
            }
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

        public static List<Claim>? VerifyToken(string? token, string secretKeyStr) 
        {
            if (string.IsNullOrEmpty(token?.Trim())) 
            {
                return null;
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring(6).Trim();
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
                if (securityToken is not null) 
                {
                    var tokenObj = handler.ReadJwtToken(token);
                    return tokenObj.Claims.ToList() ?? (new List<Claim>());
                }
            }
            catch(SecurityTokenException) 
            {
                return null;
            }
            catch
            {
                throw;
            }
            return null;
        }
    }
}
