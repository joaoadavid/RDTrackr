namespace RDTrackR.Communication.Requests.User
{
    public class RequestAdminUpdateUserJson
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Active { get; set; }
    }
}
