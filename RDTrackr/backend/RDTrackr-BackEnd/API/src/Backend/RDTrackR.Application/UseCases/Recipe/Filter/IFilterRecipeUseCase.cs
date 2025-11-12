using RDTrackR.Communication.Requests.Recipe;
using RDTrackR.Communication.Responses.Recipe;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter
{
    public interface IFilterRecipeUseCase
    {
        public Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request);
    }
}
