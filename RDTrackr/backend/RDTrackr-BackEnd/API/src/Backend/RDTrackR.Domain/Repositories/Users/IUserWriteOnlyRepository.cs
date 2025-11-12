using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Users
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(User user);
        Task UpdateAsync(User user);
    }
}
