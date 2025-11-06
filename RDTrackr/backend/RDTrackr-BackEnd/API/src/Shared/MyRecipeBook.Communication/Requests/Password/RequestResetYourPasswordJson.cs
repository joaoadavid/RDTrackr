namespace RDTrackR.Communication.Requests.Password
{
    public class RequestResetYourPasswordJson
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
