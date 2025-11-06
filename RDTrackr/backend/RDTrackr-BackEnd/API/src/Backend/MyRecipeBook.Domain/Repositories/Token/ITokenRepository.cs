using RDTrackR.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories.Token
{
    public interface ITokenRepository
    {
        Task<RefreshToken?> Get(string refreshToken);
        Task SaveNewRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenId(string tokenId);
        Task RevokeAllUserTokens(long userId);
        Task RevokeToken(string refreshTokenValue);
    }
}