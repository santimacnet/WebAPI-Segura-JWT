using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace WebApiSegura.Security
{
    /// <summary>
    /// Token validator for Authorization Request using a DelegatingHandler
    /// </summary>
    internal class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}