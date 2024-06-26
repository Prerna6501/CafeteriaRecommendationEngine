using System.Net.Sockets;
using System.Text;

namespace ClientSide
{
    public static class HandleRequest
    {
        public static async Task<string> SendRequest(string message)
        {
            try
            {
                int port = 6501;
                TcpClient client = new TcpClient("127.0.0.1", port);

                byte[] data = Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                await stream.WriteAsync(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                data = new byte[10000];
                string responseData = string.Empty;

                int bytes = await stream.ReadAsync(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);

                stream.Close();
                client.Close();

                return responseData;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"ArgumentNullException: {e}");
                throw;
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
                throw;
            }
        }
    }
}
