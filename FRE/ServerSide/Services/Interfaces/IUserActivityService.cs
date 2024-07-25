using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IUserActivityService : IGenericService<UserActivity>
    {
        public Task LogUserActivity(int userId, string activityType);
    }

}
