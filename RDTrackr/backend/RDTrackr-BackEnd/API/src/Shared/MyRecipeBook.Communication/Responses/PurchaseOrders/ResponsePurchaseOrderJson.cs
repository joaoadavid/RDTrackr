namespace RDTrackR.Communication.Responses.PurchaseOrders
{
    public class ResponsePurchaseOrderJson
    {
        public long Id { get; set; }
        public string Number { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; } = null!;
        public List<ResponsePurchaseOrderItemJson> Items { get; set; } = new();
    }
}
