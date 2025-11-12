namespace RDTrackR.Communication.Responses.User.Admin
{
    public class ResponseUserListItemJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
