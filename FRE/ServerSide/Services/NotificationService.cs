using ServerSide.Entity;
using ServerSide.Repositories;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class NotificationService : GenericService<Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository) : base(notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Task CreateNotification(int type, int id)
        {
            throw new NotImplementedException();
        }
    }
}