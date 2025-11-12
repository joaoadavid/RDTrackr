namespace RDTrackR.Communication.Responses.Overview
{
    public class ResponseOverviewJson
    {
        public int TotalProducts { get; set; }
        public int TotalWarehouses { get; set; }
        public int TotalMovements { get; set; }
        public int TotalStockItems { get; set; }
        public decimal TotalInventoryQuantity { get; set; }
    }
}
