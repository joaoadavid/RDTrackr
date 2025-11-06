using MyRecipeBook.Domain.Security.Tokens.Refresh;
using RDTrackR.Infrastructure.Security.Tokens.Refresh;

namespace CommonTestUtilities.Tokens
{
    public static class RefreshTokenGeneratorBuilder
    {
        public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
    }
}
