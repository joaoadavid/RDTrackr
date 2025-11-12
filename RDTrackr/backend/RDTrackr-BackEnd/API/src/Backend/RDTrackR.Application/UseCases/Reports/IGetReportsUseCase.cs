using RDTrackR.Communication.Responses.Reports;

namespace RDTrackR.Application.UseCases.Reports
{
    public interface IGetReportsUseCase
    {
        Task<ResponseReportsJson> Execute();
    }
}