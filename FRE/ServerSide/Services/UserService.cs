using ServerSide.Entity;
using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateUser(int id, string username, string password)
        {
            var user = _userRepository.Where(x => x.Name == username && x.Password == password && x.Id == id).FirstOrDefault();
            if (user != null)
            {
                var role = _userRepository.GetUserType(user.UserTypeId).Result;
                if (role != null)
                    return role.Name;
            }
            return "Invalid login credentials";
        }
    }
}

