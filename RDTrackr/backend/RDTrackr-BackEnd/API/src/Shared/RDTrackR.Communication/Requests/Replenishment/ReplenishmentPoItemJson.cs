namespace RDTrackR.Communication.Requests.Replenishment
{
    public class ReplenishmentPoItemJson
    {
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
