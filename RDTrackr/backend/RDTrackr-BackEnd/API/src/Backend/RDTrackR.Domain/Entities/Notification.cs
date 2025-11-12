namespace RDTrackR.Domain.Entities
{
    public class Notification
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; } = null!;
        public bool Read { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
