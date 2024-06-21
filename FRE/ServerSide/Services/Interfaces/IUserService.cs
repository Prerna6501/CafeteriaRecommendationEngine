using ServerSide.Entity;

namespace ServerSide.Services.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        public Task<string> AuthenticateUser(int id, string username, string password);
    }
}
