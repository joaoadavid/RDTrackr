namespace RDTrackR.Communication.Responses.Supplier
{
    public class ResponseSupplierJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string CreatedByName { get; set; } = null!;
    }
}
