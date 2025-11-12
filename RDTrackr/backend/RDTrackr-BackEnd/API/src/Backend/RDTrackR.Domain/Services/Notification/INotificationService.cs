namespace RDTrackR.Domain.Services.Notification
{
    public interface INotificationService
    {
        Task Notify(string message, long? targetUserId = null);
    }
}
