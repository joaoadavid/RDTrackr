using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase.Token
{
    public class RefreshTokenExpiredException : RDTrackRException
    {
        public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
