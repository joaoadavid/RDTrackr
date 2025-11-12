using RDTrackR.Communication.Responses.Audit;

namespace RDTrackR.Application.UseCases.AuditLogs
{
    public interface IGetAuditLogsUseCase
    {
        Task<List<ResponseAuditLogJson>> Execute(string? type, string? search);
    }
}