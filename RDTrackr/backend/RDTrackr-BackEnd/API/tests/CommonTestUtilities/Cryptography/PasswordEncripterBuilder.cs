using RDTrackR.Domain.Security.Cryptography;
using RDTrackR.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public static class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new BCryptNet();
    }
}
