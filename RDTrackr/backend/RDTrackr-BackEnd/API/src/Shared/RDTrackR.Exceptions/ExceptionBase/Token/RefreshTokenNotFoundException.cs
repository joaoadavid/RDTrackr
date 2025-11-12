using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase.Token
{
    public class RefreshTokenNotFoundException : RDTrackRException
    {
        public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
