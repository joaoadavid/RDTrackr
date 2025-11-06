namespace RDTrackR.Communication.Requests.Supplier
{
    public class RequestBaseSupplierJson
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Contact { get; set; }
        public string? Address { get; set; }
    }
}
