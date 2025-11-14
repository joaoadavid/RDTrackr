using Microsoft.IdentityModel.Tokens;
using RDTrackR.Domain.Entities;
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
        public string Generate(User user)
        {
            var tokenId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                SigningCredentials = new SigningCredentials(
                    SecurityKey(_signingKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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
