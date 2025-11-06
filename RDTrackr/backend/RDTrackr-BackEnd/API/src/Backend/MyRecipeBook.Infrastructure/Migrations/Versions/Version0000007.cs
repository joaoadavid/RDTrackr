using FluentMigrator;

namespace RDTrackR.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.ADD_PRODUCT_USER_RELATION, "Add CreatedByUserId to Products table")]
    public class Version0000007_AddProductUserRelation : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("CreatedByUserId").AsInt64().NotNullable()
                .ForeignKey("FK_Products_CreatedByUser", "Users", "Id");
        }
    }
}
