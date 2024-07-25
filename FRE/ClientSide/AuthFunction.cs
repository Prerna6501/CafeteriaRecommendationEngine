namespace ClientSide
{
    public class AuthFunction
    {
        public static async Task<string> AuthenticateUser(string message)
        {
            try
            {
                return await HandleRequest.SendRequest(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
