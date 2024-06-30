using Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using ServerSide.Entity;
using ServerSide.Services;
using ServerSide.Services.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSide
{
    public static class Server
    {
        public static async Task StartServer(ServiceProvider serviceProvider)
        {
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 6501);
                server.Start();

                Console.WriteLine("Server started...");

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() => HandleClient(client, serviceProvider));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
            }
            finally
            {
                server?.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        static async void HandleClient(TcpClient client, ServiceProvider serviceProvider)
        {
            var authService = serviceProvider.GetRequiredService<AuthService>();
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[256];
            string data;

            try
            {
                int i;
                while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine($"Received: {data}");
                    string[] parts = data.Split('|');
                    string requestType = parts[0];
                    string parameter = parts[1];

                    string response = await ProcessRequest(requestType, parameter, serviceProvider);
                    byte[] msg = Encoding.ASCII.GetBytes(response);

                    await stream.WriteAsync(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {response}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                client.Close();
            }
        }

        private static async Task<string> ProcessRequest(string requestType, string parameters, ServiceProvider serviceProvider)
        {
            var authService = serviceProvider.GetRequiredService<AuthService>();
            var notificationService = serviceProvider.GetRequiredService<NotificationService>();
            var menuItemService = serviceProvider.GetRequiredService<MenuItemService>();
            var feedbackService = serviceProvider.GetRequiredService<FeedbackService>();
            var chefService = serviceProvider.GetRequiredService<ChefRequestHandler>();
            

            switch (requestType.ToUpper())
            {
                case "AUTHENTICATE_USER":

                    string[] authData = parameters.Split(',');
                    if (authData.Length < 3)
                    {
                        return "Invalid parameters for authentication.";
                    }
                    int userId = int.Parse(authData[0].Trim());
                    string name = authData[1].Trim();
                    string password = authData[2].Trim();
                    return await authService.AuthenticateUser(userId, name, password);

                case "ADD_MENU_ITEM":
                    var result = await MenuItemRequestHandler.HandleAddMenuItem(parameters, menuItemService);
                    //await notificationService.CreateNotification((int)NotificationTypeEnum.NewItemAdded, int.Parse(parameters));
                    return result;

                case "UPDATE_MENU":
                    return await MenuItemRequestHandler.HandleUpdateMenuItem(parameters, menuItemService);

                case "DELETE_MENU_ITEM":
                    var response = await MenuItemRequestHandler.HandleDeleteMenuItem(parameters, menuItemService);                    
                    //await notificationService.CreateNotification((int)NotificationTypeEnum.Deleted, int.Parse(parameters));
                    return response;

                case "VIEW_MENU":
                    return await MenuItemRequestHandler.HandleViewMenuItem(menuItemService);

                case "GIVE_FEEDBACK":
                    return await FeedbackRequestHandler.HandleGiveFeedback(parameters, feedbackService);

                case "VIEW_FEEDBACK_ITEM":
                    return await FeedbackRequestHandler.HandleViewFeedbackForItem(parameters, feedbackService);

                case "VIEW_FEEDBACK_EMPLOYEE":
                    return await FeedbackRequestHandler.HandleViewFeedbackByEmployee(parameters, feedbackService);

                case "GET_RECOMMENDED_ITEMS":
                    return await chefService.GetTopMenuItemsByMealType(parameters);

                case "ROLLOUT_CHOICES":
                    return await chefService.RolloutChoices(parameters);

                case "VIEW_CHOICE_VOTING_RESULT":
                    return await chefService.GetVotingResults();

                //case "GIVE_FINAL_MENU":
                //    return await chefService();

                case "CHANGE_AVAILABILITY":
                    return await MenuItemRequestHandler.ChangeAvailability(parameters, menuItemService);
               
                //case "VIEW_MONTHLY_REPORT": optional
                //    return await chefService.ViewMonthlyReport();



                default:
                    return $"Invalid request type: {requestType}";
            }
        }


    }
}
