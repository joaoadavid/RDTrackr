using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase;
public class UnauthorizedException : RDTrackRException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}