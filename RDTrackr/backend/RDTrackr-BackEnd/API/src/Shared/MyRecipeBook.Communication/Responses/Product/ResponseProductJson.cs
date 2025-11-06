namespace RDTrackR.Communication.Responses.Product
{
    public class ResponseProductJson
    {
        public long Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UoM { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime UpdatedAt { get; set; }

        // apenas se quiser exibir no response
        public long CreatedByUserId { get; set; }
        public string? CreatedByName { get; set; }
    }

}
