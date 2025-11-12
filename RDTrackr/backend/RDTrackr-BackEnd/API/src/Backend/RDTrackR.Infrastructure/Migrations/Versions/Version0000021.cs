using FluentMigrator;

namespace RDTrackR.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.FIX_AUDITLOG_ACTIONTYPE_INT)]
    public class Version0000021_Fix_AuditLog_ActionType_Type : VersionBase
    {
        public override void Up()
        {
            Alter.Column("ActionType")
                .OnTable("AuditLogs")
                .AsInt32()
                .NotNullable();
        }
    }
}
