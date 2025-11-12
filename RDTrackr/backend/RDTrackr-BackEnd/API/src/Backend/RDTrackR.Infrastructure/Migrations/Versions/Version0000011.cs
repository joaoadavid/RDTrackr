using FluentMigrator;

namespace RDTrackR.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.ADD_WAREHOUSE_UPDATEDAT, "Add UpdatedAt to Warehouses table")]
    public class Version0000011_AddUpdatedAtToWarehouses : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("Warehouses")
                .AddColumn("UpdatedByUserId").AsInt64().Nullable()
                    .ForeignKey("FK_Warehouses_UpdatedByUser", "Users", "Id")
                .AddColumn("UpdatedAt").AsDateTime().Nullable()
                    .WithDefault(SystemMethods.CurrentDateTime);
        }
    }
}
