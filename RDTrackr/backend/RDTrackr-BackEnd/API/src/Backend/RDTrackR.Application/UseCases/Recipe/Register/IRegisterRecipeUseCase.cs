using RDTrackR.Communication.Requests.User;
using RDTrackR.Communication.Responses.Recipe;

namespace MyRecipeBook.Application.UseCases.Recipe.Register
{
    public interface IRegisterRecipeUseCase
    {
        Task<ResponseRegisteredRecipeJson> Execute(RequestRegisterRecipeFormData request);
    }
}