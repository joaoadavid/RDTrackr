using RDTrackR.Domain.Repositories;

namespace RDTrackR.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RDTrackRDbContext _dbContext;

        public UnitOfWork(RDTrackRDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
