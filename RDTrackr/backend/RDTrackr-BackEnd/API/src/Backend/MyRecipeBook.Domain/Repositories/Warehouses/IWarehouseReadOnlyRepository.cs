using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Warehouses
{
    public interface IWarehouseReadOnlyRepository
    {
        Task<List<Warehouse>> GetAllAsync();

        Task<int> CountAsync();
        Task<Warehouse?> GetByIdAsync(long id);
        
    }
}
