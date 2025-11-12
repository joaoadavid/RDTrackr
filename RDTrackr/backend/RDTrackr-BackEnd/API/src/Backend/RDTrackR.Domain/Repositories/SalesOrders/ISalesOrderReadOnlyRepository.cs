using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.SalesOrders
{
    public interface ISalesOrderReadOnlyRepository
    {
        Task<List<SalesOrder>> GetRecentAsync(int days);
    }

}
