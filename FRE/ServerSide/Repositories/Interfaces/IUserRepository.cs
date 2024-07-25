using ServerSide.Entity;

namespace ServerSide.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<UserType> GetUserType(int id);
    }
}
