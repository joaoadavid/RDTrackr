using Azure.Messaging.ServiceBus;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Services.ServiceBus;

namespace RDTrackR.Infrastructure.Services.ServiceBus
{
    public class DeleteUserQueue : IDeleteUserQueue
    {
        private readonly ServiceBusSender _serviceBusSender;

        public DeleteUserQueue(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }
        public async Task SendMessage(User user)
        {
            await _serviceBusSender.SendMessageAsync(
                new ServiceBusMessage(user.UserIdentifier.ToString()));
        }
    }
}
