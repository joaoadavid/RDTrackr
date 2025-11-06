using Sqids;

namespace CommonTestUtilities.IdEncryption;
public static class IdEncripterBuilder
{
    public static SqidsEncoder<long> Build()
    {
        return new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = "8ISgBfzHo0JLwMUckEtlpqWG9F54VQdAehrZbixa3XOvY7jm2KNTDCPuRs61ny"
        });
    }
}