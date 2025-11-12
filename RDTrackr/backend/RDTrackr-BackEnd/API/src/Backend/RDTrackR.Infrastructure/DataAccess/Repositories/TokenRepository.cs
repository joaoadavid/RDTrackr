using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Repositories.Token;
using RDTrackR.Domain.Entities;

namespace RDTrackR.Infrastructure.DataAccess.Repositories;
public class TokenRepository : ITokenRepository
{
    private readonly RDTrackRDbContext _dbContext;

    public TokenRepository(RDTrackRDbContext dbContext) => _dbContext = dbContext;

    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await _dbContext
            .RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    }

    public async Task<RefreshToken?> GetByTokenId(string tokenId)
    {
        return await _dbContext
            .RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.TokenId == tokenId && token.IsRevoked == false);
    }


    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

        _dbContext.RefreshTokens.RemoveRange(tokens);

        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task RevokeAllUserTokens(long userId)
    {
        var tokens = _dbContext.RefreshTokens
            .Where(t => t.UserId == userId && !t.IsRevoked);

        foreach (var token in tokens)
            token.IsRevoked = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task RevokeToken(string refreshTokenValue)
    {
        var token = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Value == refreshTokenValue);

        if (token is not null)
        {
            token.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
        }
    }


}