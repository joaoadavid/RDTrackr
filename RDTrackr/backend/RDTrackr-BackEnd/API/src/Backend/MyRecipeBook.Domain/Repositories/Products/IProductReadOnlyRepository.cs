using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Products
{
    public interface IProductReadOnlyRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(long id);
        Task<int> CountAsync();
        Task<bool> ExistsActiveProductWithSku(string sku);
    }
}
