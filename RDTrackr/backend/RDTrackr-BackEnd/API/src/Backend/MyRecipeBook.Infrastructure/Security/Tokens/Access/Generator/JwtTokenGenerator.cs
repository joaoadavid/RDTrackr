using Microsoft.IdentityModel.Tokens;
using RDTrackR.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RDTrackR.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly uint _expirationInMinutes;
        private readonly string _signingKey;

        public JwtTokenGenerator(uint expirationInMinutes, string signingKey)
        {
            _expirationInMinutes = expirationInMinutes;
            _signingKey = signingKey;
        }
        public string Generate(Guid userIdentifier)
        {
            var tokenId = Guid.NewGuid().ToString(); // novo identificador do AccessToken

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString()),
                new Claim("TokenId", tokenId)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        public string GenerateWithTokenId(Guid userIdentifier, string tokenId)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Sid, userIdentifier.ToString()),
        new Claim("TokenId", tokenId)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

    }
}
