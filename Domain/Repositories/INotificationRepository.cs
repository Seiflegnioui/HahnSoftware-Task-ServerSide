using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{

    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification, CancellationToken cancellationToken);
        Task<List<Notification>> GetNotifications(int notified, CancellationToken cancellationToken);


}
}