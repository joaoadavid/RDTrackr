using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RDTrackR.API.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var sub = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return Guid.Parse(sub);
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Role) ?? "User";
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }
    }
}