using RDTrackR.Communication.Requests.Recipe;

namespace MyRecipeBook.Application.UseCases.Recipe.Update
{
    public interface IUpdateRecipeUseCase
    {
        Task Execute(long recipeId, RequestRecipeJson request);
    }
}
