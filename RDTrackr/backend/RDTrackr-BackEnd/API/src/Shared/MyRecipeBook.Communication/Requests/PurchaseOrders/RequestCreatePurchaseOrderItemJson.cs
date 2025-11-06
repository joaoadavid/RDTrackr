namespace RDTrackR.Communication.Requests.PurchaseOrders
{
    public class RequestCreatePurchaseOrderItemJson
    {
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
