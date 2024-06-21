using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface INotificationService : IGenericService<Notification>
    {
        public Task CreateNotification (int type, int id);
    }
}
