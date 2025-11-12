using System.Security.Cryptography;
using System.Text;

namespace RDTrackR.Domain.Security.Cryptography
{
    public interface IPasswordEncripter
    {
        public string Encrypt(string password);
        public bool IsValid(string password, string passwordHase);
    }
}
