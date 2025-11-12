using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Products
{
    public interface IProductWriteOnlyRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
