using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> AuthenticateUser(int Id, string username, string password)
        {
            return await _userService.AuthenticateUser(Id, username, password);
        }
    }
}
