using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using RDTrackR.Communication.Responses.Error;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories.Users;
using RDTrackR.Domain.Security.Tokens;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using System.Security.Claims;

namespace RDTrackR.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;
        private readonly string _requiredRoles;
        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository, string roles)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = repository;
            _requiredRoles = roles;
        }
        //public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        //{
        //    try
        //    {
        //        var token = TokenOnRequest(context);
        //        var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

        //        var user = await _repository.GetByIdentifier(userIdentifier);

        //        var exist = await _repository.ExistActiveUserWithIdenfier(userIdentifier);

        //        if (exist.IsFalse() || user == null)
        //        {
        //            throw new UnauthorizedException(ResourceMessagesException.USER_WITHOU_PERMISSION_ACCESS_RESOURCE);
        //        }

        //        //var claims = new List<Claim>
        //        //{
        //        //    new Claim("sub", user.Id.ToString()),          // ← Aqui usamos o ID numérico real
        //        //    new Claim(ClaimTypes.Name, user.Name ?? "")    // ← Evita null
        //        //};

        //        //var identity = new ClaimsIdentity(claims, "Bearer");
        //        //context.HttpContext.User = new ClaimsPrincipal(identity);
        //        var claims = new List<Claim>
        //        {
        //            new Claim("sub", user.Id.ToString()),
        //            new Claim(ClaimTypes.Name, user.Name ?? ""),
        //            new Claim(ClaimTypes.Role, user.Role)
        //        };

        //        var identity = new ClaimsIdentity(claims, "Bearer");
        //        context.HttpContext.User = new ClaimsPrincipal(identity);


        //    }
        //    catch (SecurityTokenExpiredException)
        //    {
        //        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
        //        {
        //            TokenExpired = true,
        //        });
        //    }
        //    catch (RDTrackRException ex)
        //    {
        //        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        //    }
        //    catch
        //    {
        //        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOU_PERMISSION_ACCESS_RESOURCE));
        //    }
        //}

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                // Validar token e buscar usuário
                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
                var user = await _repository.GetByIdentifier(userIdentifier);

                if (user == null)
                {
                    throw new UnauthorizedException("User not found");
                }

                // Criar Claims
                var claims = new List<Claim>
                {
                    new Claim("sub", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name ?? ""),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, "Bearer");
                context.HttpContext.User = new ClaimsPrincipal(identity);

                // SE TIVER ROLES NO ATRIBUTO, VALIDAR AQUI
                if (!string.IsNullOrWhiteSpace(_requiredRoles))
                {
                    var allowed = _requiredRoles.Split(',').Select(r => r.Trim()).ToList();
                    var userRole = user.Role;

                    if (!allowed.Contains(userRole))
                    {
                        throw new UnauthorizedException("User does not have permission to access this resource");
                    }
                }
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedObjectResult(
                    new ResponseErrorJson("USER_WITHOUT_PERMISSION")
                );
            }
        }
        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authentication))
            {
                throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}

