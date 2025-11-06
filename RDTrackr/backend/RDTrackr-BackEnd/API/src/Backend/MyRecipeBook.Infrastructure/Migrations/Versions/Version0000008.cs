using RDTrackR.Infrastructure.Migrations.Versions;
using RDTrackR.Infrastructure.Migrations;
using FluentMigrator;

[Migration(DatabaseVersions.TABLE_WAREHOUSES, "Create Warehouses table")]
public class Version0000008_Create_Warehouses : VersionBase
{
    public override void Up()
    {
        CreateTable("Warehouses")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Location").AsString(255).NotNullable()
            .WithColumn("Capacity").AsInt32().NotNullable()
            .WithColumn("Items").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Utilization").AsDecimal(5, 2).NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedByUserId").AsInt64().NotNullable()
                .ForeignKey("FK_Warehouses_CreatedByUser", "Users", "Id");
    }
}
