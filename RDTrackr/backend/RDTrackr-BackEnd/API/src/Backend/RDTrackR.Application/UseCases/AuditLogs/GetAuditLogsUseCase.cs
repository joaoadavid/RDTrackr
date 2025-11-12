using RDTrackR.Communication.Responses.Audit;
using RDTrackR.Domain.Repositories.Audit;

namespace RDTrackR.Application.UseCases.AuditLogs
{
    public class GetAuditLogsUseCase : IGetAuditLogsUseCase
    {
        private readonly IAuditLogRepository _repo;

        public GetAuditLogsUseCase(IAuditLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ResponseAuditLogJson>> Execute(string? type, string? search)
        {
            var logs = await _repo.GetRecentAsync(type, search);

            return logs
                .Select(log => new ResponseAuditLogJson
                {
                    User = log.UserName,
                    Action = log.Description,
                    Type = log.ActionType.ToString(),
                    Date = log.Timestamp
                })
                .ToList();
        }
    }
}
