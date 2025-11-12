using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
