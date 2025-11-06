using RDTrackR.Communication.Responses.Recipe;

namespace RDTrackR.Application.UseCases.Dashboard
{
    public interface IGetDashboardUseCase
    {
        Task<ResponseRecipesJson> Execute();
    }
}