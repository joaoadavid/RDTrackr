using FluentMigrator;

namespace RDTrackR.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_PURCHASE_ORDER_ITEMS)]
    public class Version0000015 : VersionBase
    {
        public override void Up()
        {
            CreateTable("PurchaseOrderItems")
                .WithColumn("PurchaseOrderId").AsInt64().NotNullable().ForeignKey("FK_POItems_PO", "PurchaseOrders", "Id")
                .WithColumn("ProductId").AsInt64().NotNullable().ForeignKey("FK_POItems_Product", "Products", "Id")
                .WithColumn("Quantity").AsDecimal(18, 2).NotNullable()
                .WithColumn("UnitPrice").AsDecimal(18, 2).NotNullable();
        }
    }   

}
