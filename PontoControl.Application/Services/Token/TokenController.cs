using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PontoControl.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "eml";
        private readonly double _timeLifeTokenInMinutes;
        private readonly string _securityKey;

        public TokenController(double timeLifeTokenInMinutes, string securityKey)
        {
            _timeLifeTokenInMinutes = timeLifeTokenInMinutes;
            _securityKey = securityKey;
        }

        public string TokenGenerate(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(EmailAlias, userEmail)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_timeLifeTokenInMinutes),
                SigningCredentials = new SigningCredentials(SymmetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public ClaimsPrincipal TokenValidate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SymmetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false
            };


            var claims = tokenHandler.ValidateToken(token, validationParameters, out _);
            return claims;
        }

        public string GetUserEmail(string token)
        {
            var claims = TokenValidate(token);
            return claims.FindFirst(EmailAlias).Value;
        }

        private SymmetricSecurityKey SymmetricKey()
        {
            var symmetricKey = Convert.FromBase64String(_securityKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
