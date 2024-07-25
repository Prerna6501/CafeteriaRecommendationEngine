using Common.CustomExceptions;
using Common.Enums;
using Common.Utilities;
using ServerSide.Entity;
using ServerSide.Services.Interfaces;

namespace ServerSide.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserActivityService _userActivityService;

        public AuthService(IUserService userService, IUserActivityService userActivityService)
        {
            _userService = userService;
            _userActivityService = userActivityService;
        }

        public async Task<string> AuthenticateUser(string parameters)
        {
            try
            {
                string[] authData = parameters.Split(',');
                if (authData.Length < 3)
                {
                    return "Invalid parameters for authentication.";
                }
                int userId = int.Parse(authData[0].Trim());
                string name = authData[1].Trim();
                string password = authData[2].Trim();

                var response = await _userService.AuthenticateUser(userId, name, password);
                await _userActivityService.LogUserActivity(userId, ActivityTypeEnum.Login.ToString());
                
                return ResponseUtils.CreateSuccessJsonResponse(response);
            }
            catch (AuthenticateException ex)
            {
                throw new AuthenticateException(ex.Message);
            }
        }

        public async Task<string> LogoutUser(string parameter)
        {
            int userId = int.Parse(parameter.Trim());
            await _userActivityService.LogUserActivity(userId, ActivityTypeEnum.Logout.ToString());
            
            return ResponseUtils.CreateSuccessJsonResponse("Logout..");
        }
    }
}
