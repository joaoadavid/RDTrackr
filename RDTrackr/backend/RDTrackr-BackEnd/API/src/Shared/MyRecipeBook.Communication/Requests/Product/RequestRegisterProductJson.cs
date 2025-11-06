namespace RDTrackR.Communication.Requests.Product
{
    public class RequestRegisterProductJson
    {
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UoM { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ReorderPoint { get; set; }
    }
}
