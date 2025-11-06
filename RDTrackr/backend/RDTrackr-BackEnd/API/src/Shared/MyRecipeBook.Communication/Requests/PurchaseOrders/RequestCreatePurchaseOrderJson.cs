namespace RDTrackR.Communication.Requests.PurchaseOrders
{
    public class RequestCreatePurchaseOrderJson
    {
        public long SupplierId { get; set; }
        public List<RequestCreatePurchaseOrderItemJson> Items { get; set; } = new();
    }
}
