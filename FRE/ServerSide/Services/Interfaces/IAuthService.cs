namespace ServerSide.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> AuthenticateUser(string parameters);
        public Task<string> LogoutUser(string parameter);
    }
}
