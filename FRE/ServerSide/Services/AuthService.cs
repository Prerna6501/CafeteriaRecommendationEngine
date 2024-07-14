using Common.CustomExceptions;
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
            try
            {
                return await _userService.AuthenticateUser(Id, username, password);
            }
            catch (AuthenticateException ex)
            {
                throw new AuthenticateException(ex.Message);
            }
        }
    }
}
