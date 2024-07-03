using ServerSide.Entity;
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

        public async Task CreateNotification(int typeId, string message)
        {
            Notification notification = new Notification
            {
                NotificationTypeId = typeId,
                Message = message,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            await _notificationRepository.CreateAsync(notification);
        }
    }
}