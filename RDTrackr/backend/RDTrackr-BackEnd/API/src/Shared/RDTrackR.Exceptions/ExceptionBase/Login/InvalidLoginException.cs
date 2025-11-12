using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase.Login;

public class InvalidLoginException : RDTrackRException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}