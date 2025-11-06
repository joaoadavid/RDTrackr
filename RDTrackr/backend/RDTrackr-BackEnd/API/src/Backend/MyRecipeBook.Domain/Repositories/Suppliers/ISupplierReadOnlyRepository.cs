using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Suppliers
{
    public interface ISupplierReadOnlyRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(long id);
        Task<bool> ExistsWithEmail(string email);
        Task<bool> ExistsSupplierWithEmail(string email, long id);
    }
}
