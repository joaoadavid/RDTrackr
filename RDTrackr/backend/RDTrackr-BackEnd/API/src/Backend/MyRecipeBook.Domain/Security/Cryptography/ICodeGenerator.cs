namespace RDTrackR.Domain.Security.Cryptography
{
    public interface ICodeGenerator
    {
        string Generate(int length);
    }
}
