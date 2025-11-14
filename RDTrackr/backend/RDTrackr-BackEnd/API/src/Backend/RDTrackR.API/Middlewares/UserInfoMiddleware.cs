using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RDTrackR.API.Middlewares
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                context.Items["UserId"] = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
                context.Items["UserRole"] = user.FindFirstValue(ClaimTypes.Role);
                context.Items["UserName"] = user.FindFirstValue(ClaimTypes.Name);
            }

            await _next(context);
        }
    }
}