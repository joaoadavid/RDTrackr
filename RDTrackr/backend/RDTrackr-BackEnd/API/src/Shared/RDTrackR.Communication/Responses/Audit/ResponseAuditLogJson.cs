namespace RDTrackR.Communication.Responses.Audit
{
    public class ResponseAuditLogJson
    {
        public string User { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
