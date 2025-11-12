using RDTrackR.Communication.Enums;

namespace RDTrackR.Communication.Requests.Movements
{
    public class RequestGetMovementsJson
    {
        public long? WarehouseId { get; set; }
        public MovementType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
