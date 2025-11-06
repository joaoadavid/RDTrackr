using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Warehouses;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class WarehouseRepository : IWarehouseReadOnlyRepository, IWarehouseWriteOnlyRepository
    {
        private readonly RDTrackRDbContext _context;

        public WarehouseRepository(RDTrackRDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
        }

        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
        }

        public async Task DeleteAsync(Warehouse warehouse) 
        {
            _context.Warehouses.Remove(warehouse);
        }
        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses
                .AsNoTracking()
                .Include(w => w.StockItems)
                .Include(w => w.CreatedBy)
                .OrderBy(w => w.Name)
                .ToListAsync();
        }


        public async Task<Warehouse?> GetByIdAsync(long id)
        {
            return await _context.Warehouses
                .AsNoTracking()
                .Include(w => w.StockItems)
                .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}