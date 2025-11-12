using RDTrackR.Communication.Responses.Overview;

namespace RDTrackR.Application.UseCases.Overview.Get
{
    public interface IGetOverviewUseCase
    {
        Task<ResponseOverviewJson> Execute();
    }
}