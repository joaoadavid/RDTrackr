using Microsoft.EntityFrameworkCore;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Products;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class ProductRepository : IProductReadOnlyRepository, IProductWriteOnlyRepository
    {
        private readonly RDTrackRDbContext _context;

        public ProductRepository(RDTrackRDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await Task.CompletedTask;
        }

        public async Task<List<Product>> GetAllAsync(User user)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.CreatedBy)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product?> GetByIdAsync(long id,User user)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.CreatedByUserId == user.Id);
        }

        public async Task<bool> ExistsActiveProductWithSku(string sku)
        {
            return await _context.Products.AsNoTracking().AnyAsync(p => p.Sku == sku && p.Active);
        }
    }
}
