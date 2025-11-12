using RDTrackR.Domain.Enums;

namespace RDTrackR.Domain.Entities
{
    public class SalesOrder : EntityBase
    {
        public string CustomerName { get; set; } = null!;
        public decimal Total { get; set; }
        public SalesOrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
