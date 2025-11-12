using Microsoft.AspNetCore.Http;
using RDTrackR.Communication.Requests.Recipe;

namespace RDTrackR.Communication.Requests.User
{
    public class RequestRegisterRecipeFormData : RequestRecipeJson
    {
        public IFormFile? Image { get; set; }
    }
}
