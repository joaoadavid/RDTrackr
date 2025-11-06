namespace RDTrackR.Communication.Responses.Replenishment
{
    public class ResponseReplenishmentItemJson
    {
        public long ProductId { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Uom { get; set; } = null!;
        public decimal CurrentStock { get; set; }
        public decimal ReorderPoint { get; set; }
        public decimal DailyConsumption { get; set; }
        public int LeadTimeDays { get; set; }
        public decimal SuggestedQty { get; set; }
        public bool IsCritical { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
