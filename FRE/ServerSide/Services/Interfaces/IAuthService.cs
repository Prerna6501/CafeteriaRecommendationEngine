namespace ServerSide.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> AuthenticateUser(int Id, string username, string password);
    }
}
