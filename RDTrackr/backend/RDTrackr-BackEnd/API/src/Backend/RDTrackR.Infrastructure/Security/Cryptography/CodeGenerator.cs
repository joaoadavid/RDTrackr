using RDTrackR.Domain.Security.Cryptography;
using System.Security.Cryptography;

namespace RDTrackR.Infrastructure.Security.Cryptography
{
    public class CodeGenerator : ICodeGenerator
    {
        public string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)])
                .ToArray());
        }
    }
}
