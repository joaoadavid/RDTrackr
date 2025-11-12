using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Enums;

namespace RDTrackR.Domain.Repositories.Movements
{
    public interface IMovementReadOnlyRepository
    {
        Task<List<Movement>> GetAllAsync();
        Task<Movement?> GetByIdAsync(long id);
        Task<int> CountAsync();
        Task<List<Movement>> GetByTypeAsync(string type);
        Task<List<Movement>> GetFilteredAsync(long? warehouseId, MovementType? type, DateTime? startDate, DateTime? endDate);
    }
}
