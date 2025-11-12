namespace RDTrackR.Communication.Responses.Reports
{
    public class ResponseRecentPurchaseOrderJson
    {
        public long Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
