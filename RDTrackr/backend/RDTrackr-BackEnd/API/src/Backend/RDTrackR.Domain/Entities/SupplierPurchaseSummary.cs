namespace RDTrackR.Domain.Entities
{
    public class SupplierPurchaseSummary
    {
        public string SupplierName { get; set; } = null!;
        public decimal TotalPurchased { get; set; }
    }
}
