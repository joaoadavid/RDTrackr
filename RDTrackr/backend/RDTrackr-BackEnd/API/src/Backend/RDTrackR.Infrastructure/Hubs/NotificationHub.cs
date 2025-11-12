using Microsoft.AspNetCore.SignalR;

namespace RDTrackR.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
