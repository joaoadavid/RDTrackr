namespace RDTrackR.Infrastructure.Migrations
{
    public abstract class DatabaseVersions
    {
        public const int TABLE_USER = 1;
        public const int IMAGES_FOR_RECIPES = 3;
        public const int TABLE_REFRESH_TOKEN = 4;
        public const int TABLE_USER_FORGOT_PASSWORD = 5;
        public const int TABLE_RDTRACKR_PRODUCTS = 6;
        public const int ADD_PRODUCT_USER_RELATION = 7;
        public const int TABLE_WAREHOUSES = 8;
        public const int TABLE_MOVEMENTS = 9;
        public const int TABLE_STOCKITEMS = 10;
        public const int ADD_WAREHOUSE_UPDATEDAT = 11;
        public const int ALTER_MOVEMENT_TYPE_COLUMN = 12;
        public const int TABLE_SUPPLIERS = 13;
        public const int TABLE_PURCHASE_ORDERS = 14;
        public const int TABLE_AUDIT_LOG = 15;
        public const int ADD_PRODUCT_REPLENISHMENT_FIELDS = 16;
        public const int TABLE_PURCHASE_ORDER_ITEMS = 17;
        public const int TABLE_NOTIFICATIONS = 18;
        public const int ALTER_PURCHASE_ORDERS_NUMBER_TO_IDENTITY = 19;
        public const int ALTER_PURCHASE_ORDERS_STAUTS_TO_INT32 = 20;
        public const int FIX_AUDITLOG_ACTIONTYPE_INT = 21;
        public const int ADD_USER_ROLE_COLUMN = 22;

    }
}
