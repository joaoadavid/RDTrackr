using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.SalesOrders;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class SalesOrderReadOnlyRepository : ISalesOrderReadOnlyRepository
    {
        private readonly RDTrackRDbContext _context;

        public SalesOrderReadOnlyRepository(RDTrackRDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesOrder>> GetRecentAsync(int days)
        {
            var dateLimit = DateTime.UtcNow.AddDays(-days);

            return await _context.SalesOrders
                .Where(o => o.CreatedAt >= dateLimit)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
