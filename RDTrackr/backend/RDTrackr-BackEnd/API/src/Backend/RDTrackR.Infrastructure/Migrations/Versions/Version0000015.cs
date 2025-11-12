using FluentMigrator;

namespace RDTrackR.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_AUDIT_LOG, "Create AuditLog table")]
    public class Version0000015 : VersionBase
    {
        public override void Up()
        {
            CreateTable("AuditLogs")
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("UserName").AsString(150).NotNullable()
                .WithColumn("ActionType").AsString(30).NotNullable()
                .WithColumn("Description").AsString(500).NotNullable()
                .WithColumn("Timestamp").AsDateTime().NotNullable();
        }

    }

}
