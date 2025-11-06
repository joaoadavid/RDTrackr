using MyRecipeBook.Domain.Security.Tokens.Refresh;

namespace RDTrackR.Infrastructure.Security.Tokens.Refresh
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
