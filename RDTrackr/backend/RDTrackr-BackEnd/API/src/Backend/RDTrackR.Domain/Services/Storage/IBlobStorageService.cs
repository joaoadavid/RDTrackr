using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Services.Storage
{
    public interface IBlobStorageService
    {
        Task Upload(User user, Stream file, string filename);
        Task Delete(User user, string filename);
        Task<string> GetFileUrl(User user, string filename);
        Task DeleteContainer(Guid userIdentifier);
    }
}
