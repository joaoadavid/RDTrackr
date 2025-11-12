using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Warehouses
{
    public interface IWarehouseWriteOnlyRepository
    {
        Task AddAsync(Warehouse warehouse);
        Task UpdateAsync(Warehouse warehouse);
        Task DeleteAsync(Warehouse warehouse);
    }
}
