using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Movements
{
    public interface IMovementWriteOnlyRepository
    {
        Task AddAsync(Movement movement);
    }
}
