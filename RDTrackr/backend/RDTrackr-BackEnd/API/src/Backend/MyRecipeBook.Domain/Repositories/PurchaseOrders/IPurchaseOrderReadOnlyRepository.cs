using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.PurchaseOrders
{
    public interface IPurchaseOrderReadOnlyRepository
    {
        public Task<List<PurchaseOrder>> GetAllAsync();
        Task<PurchaseOrder?> GetByIdAsync(long id);
    }
}
