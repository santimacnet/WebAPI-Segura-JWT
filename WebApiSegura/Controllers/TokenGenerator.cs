using System;
using System.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebApiSegura.Controllers
{
    /// <summary>
    /// JWT Token generator class using "secret-key"
    /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    /// </summary>
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(string username)
        {
            // appsetting for JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];
            var symmetricKey = Convert.FromBase64String(secretKey);

            // create a claimsIdentity 
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });
            var securityKey = new SymmetricSecurityKey(symmetricKey);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create token to the user 
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}
