namespace RDTrackR.Communication.Requests.PurchaseOrders
{
    public class RequestUpdatePurchaseOrderItemsJson
    {
        public List<RequestUpdatePurchaseOrderItemJson> Items { get; set; } = new();
    }
}
