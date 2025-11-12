namespace RDTrackR.Communication.Requests.StockItem
{
    public class RequestRegisterStockItemJson
    {
        public long ProductId { get; set; }
        public long WarehouseId { get; set; }
        public decimal Quantity { get; set; }
    }
}
