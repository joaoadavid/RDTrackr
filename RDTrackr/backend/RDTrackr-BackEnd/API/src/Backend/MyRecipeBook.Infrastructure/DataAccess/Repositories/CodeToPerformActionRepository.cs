using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Password;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class CodeToPerformActionRepository : ICodeToPerformActionRepository
    {
        private readonly RDTrackRDbContext _dbContext;

        public CodeToPerformActionRepository(RDTrackRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CodeToPerformAction?> GetByCode(string code)
        {
            return await _dbContext.CodeToPerformActions
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Value == code);
        }

        public void DeleteAllUserCodes(User user)
        {
            var codes = _dbContext.CodeToPerformActions
                .Where(c => c.UserId == user.Id)
                .ToList();

            _dbContext.CodeToPerformActions.RemoveRange(codes);
        }

        public async Task Add(CodeToPerformAction code)
        {
            await _dbContext.CodeToPerformActions.AddAsync(code);
        }
    }
}
