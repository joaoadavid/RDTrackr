using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Suppliers
{
    public interface ISupplierReadOnlyRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(long id,User user);
        Task<bool> ExistsWithEmail(string email);
        Task<bool> ExistsSupplierWithEmail(string email, long id);
    }
}
