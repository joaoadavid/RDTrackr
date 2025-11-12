using Microsoft.AspNetCore.Mvc;
using RDTrackR.API.Filters;

namespace RDTrackR.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
