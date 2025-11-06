namespace RDTrackR.Communication.Requests.Warehouse
{    public class RequestRegisterWarehouseJson
     {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        public int Items { get; set; }
     }
}
