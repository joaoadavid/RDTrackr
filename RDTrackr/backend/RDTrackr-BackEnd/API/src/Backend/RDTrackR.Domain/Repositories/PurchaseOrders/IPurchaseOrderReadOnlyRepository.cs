using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.PurchaseOrders
{
    public interface IPurchaseOrderReadOnlyRepository
    {
        public Task<List<PurchaseOrder>> Get(User user);
        Task<PurchaseOrder?> GetByIdAsync(long id,User user);
        Task<List<PurchaseOrder>> GetRecentAsync(int days);
        Task<decimal> GetTotalPurchasedLast30Days();
        Task<int> GetPendingCount();
        Task<List<SupplierPurchaseSummary>> GetTopSuppliers(int topN);
    }
}
