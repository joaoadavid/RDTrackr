namespace RDTrackR.Communication.Requests.Token
{
    public class RequestNewTokenJson
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string? TokenId { get; set; }

    }
}
