using RDTrackR.Domain.Enums;

namespace RDTrackR.Domain.Services.Audit
{
    public interface IAuditService
    {
        Task Log(AuditActionType type, string description);
    }
}
