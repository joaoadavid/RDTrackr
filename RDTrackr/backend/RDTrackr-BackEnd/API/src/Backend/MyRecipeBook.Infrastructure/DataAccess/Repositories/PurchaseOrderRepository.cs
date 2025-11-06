using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.PurchaseOrders;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class PurchaseOrderRepository :
    IPurchaseOrderWriteOnlyRepository, IPurchaseOrderReadOnlyRepository
    {
        private readonly RDTrackRDbContext _context;

        public PurchaseOrderRepository(RDTrackRDbContext context) => _context = context;

        public async Task AddAsync(PurchaseOrder order) =>
            await _context.PurchaseOrders.AddAsync(order);

        public async Task<List<PurchaseOrder>> GetAllAsync() =>
            await _context.PurchaseOrders
                .Include(p => p.Supplier)
                .Include(p => p.CreatedBy)
                .Include(p => p.Items).ThenInclude(i => i.Product)
                .AsNoTracking().ToListAsync();

        public Task UpdateAsync(PurchaseOrder order)
        {
            _context.PurchaseOrders.Update(order);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(PurchaseOrder order)
        {
            _context.PurchaseOrders.Remove(order);
            return Task.CompletedTask;
        }

        public async Task<PurchaseOrder?> GetByIdAsync(long id)
        {
            return await _context.PurchaseOrders.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
