using System.Net;

namespace RDTrackR.Exceptions.ExceptionBase;
public abstract class RDTrackRException : SystemException
{
    protected RDTrackRException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}