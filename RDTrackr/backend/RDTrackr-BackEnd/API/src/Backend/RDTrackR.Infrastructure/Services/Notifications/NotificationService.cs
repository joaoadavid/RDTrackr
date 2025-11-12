using Microsoft.AspNetCore.SignalR;
using RDTrackR.Domain.Context;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Notifications;
using RDTrackR.Domain.Services.Notification;
using RDTrackR.Infrastructure.Hubs;

namespace RDTrackR.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IUserContext _user;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(
            INotificationRepository repo,
            IUserContext user,
            IHubContext<NotificationHub> hub)
        {
            _repo = repo;
            _user = user;
            _hub = hub;
        }

        public async Task Notify(string message, long? targetUserId = null)
        {
            var userId = targetUserId ?? _user.UserId;

            await _repo.AddAsync(new Notification
            {
                UserId = userId,
                Message = message,
                Read = false,
                CreatedAt = DateTime.UtcNow
            });

            await _hub.Clients.User(userId.ToString())
                .SendAsync("ReceiveNotification", message);
        }
    }
}
