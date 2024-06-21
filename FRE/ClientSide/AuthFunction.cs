namespace ClientSide
{
    public class AuthFunction
    {
        public static async Task<string> AuthenticateUser(string message)
        {
            try
            {
                string response = await HandleRequest.SendRequest(message);
                Console.WriteLine(response);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
