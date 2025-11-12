namespace RDTrackR.Communication.Requests.PurchaseOrders
{
    public class RequestUpdatePurchaseOrderItemJson
    {
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
