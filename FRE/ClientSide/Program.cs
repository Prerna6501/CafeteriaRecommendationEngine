using Azure;
using Common.CustomExceptions;
using Common.Models;
using Common.Utilities;
using Newtonsoft.Json;

namespace ClientSide
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool isAuthenticated = false;
            string role = string.Empty;
            string userId = string.Empty;

            while (!isAuthenticated)
            {
                UserCredentials credentials = GetUserCredentials();
                string loginMessage = $"AUTHENTICATE_USER|{credentials.UserId},{credentials.Name},{credentials.Password}";

                try
                {
                    var response = await AuthFunction.AuthenticateUser(loginMessage);
                    
                    if (ResponseUtils.HandleResponse(response, out var message))
                    {
                        isAuthenticated = true;
                        userId = credentials.UserId;
                        role = message;
                    }
                    else
                    {
                        Console.WriteLine("\nTry again......\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
            await FunctionalityMenu.GetFunctionalityMenu(role, userId);
        }

        static UserCredentials GetUserCredentials()
        {
            Console.Write("Enter User ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            return new UserCredentials { UserId = id, Name = name, Password = password };
        }
    }
}


