using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase
{
    public class NotFoundException : RDTrackRException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
