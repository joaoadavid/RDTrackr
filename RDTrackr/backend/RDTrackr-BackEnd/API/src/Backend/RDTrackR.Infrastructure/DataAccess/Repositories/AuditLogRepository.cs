using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Audit;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly RDTrackRDbContext _context;

        public AuditLogRepository(RDTrackRDbContext context) => _context = context;

        public async Task AddAsync(AuditLog log)
        {
            await _context.AuditLogs.AddAsync(log);
        }

        public async Task<List<AuditLog>> GetRecentAsync(string? filterType, string? search)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(filterType))
                query = query.Where(x => x.ActionType.ToString() == filterType);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Description.Contains(search));

            return await query
                .OrderByDescending(x => x.Timestamp)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
