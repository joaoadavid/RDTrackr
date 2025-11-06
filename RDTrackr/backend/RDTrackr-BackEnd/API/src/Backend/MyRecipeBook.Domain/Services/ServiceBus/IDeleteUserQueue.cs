using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Services.ServiceBus
{
    public interface IDeleteUserQueue
    {
        Task SendMessage(User user);
    }
}
