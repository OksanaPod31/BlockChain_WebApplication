using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlockchainApp.Web.Server
{
    public class ChatJwtValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;
        public int MaximumTokenSizeInBytes { get; set; } = int.MaxValue;
        public string Audience { get; }
        public string Issuer { get; }
        public string SecretKey { get; }

        public ChatJwtValidator(TokenParameters tokenParameters)
        {
            Audience = tokenParameters.Audience;
            Issuer = tokenParameters.Issuer;
            SecretKey = tokenParameters.SecretKey;
        }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = Issuer,
                ValidAudience = Audience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            };
            try
            {
                var claimsPrincipal = handler.ValidateToken(securityToken, tokenValidationParameters, out validatedToken);
                return claimsPrincipal;
            }
            catch(Exception ex)
            {
                validatedToken = new JwtSecurityToken();
                return new ClaimsPrincipal();
            }
        }
    }
}
