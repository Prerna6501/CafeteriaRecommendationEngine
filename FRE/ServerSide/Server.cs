using Common.CustomExceptions;
using Common.Utilities;
using Microsoft.Extensions.DependencyInjection;
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
            var requestHandler = serviceProvider.GetRequiredService<IRequestHandler>();

            try
            {
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

                        var response = await authService.AuthenticateUser(userId, name, password);
                        return ResponseUtils.CreateSuccessJsonResponse(response);

                    case "ADD_MENU_ITEM":
                        var addMenuResponse = await MenuItemRequestHandler.HandleAddMenuItem(parameters, menuItemService);
                        return ResponseUtils.CreateSuccessJsonResponse(addMenuResponse);

                    case "UPDATE_MENU":
                        return await MenuItemRequestHandler.HandleUpdateMenuItem(parameters, menuItemService);

                    case "DELETE_MENU_ITEM":
                        return await MenuItemRequestHandler.HandleDeleteMenuItem(parameters, menuItemService);

                    case "VIEW_MENU":
                        return await MenuItemRequestHandler.HandleViewMenuItem(menuItemService);

                    case "GIVE_FEEDBACK":
                        return await FeedbackRequestHandler.HandleGiveFeedback(parameters, feedbackService);

                    case "VIEW_FEEDBACK_ITEM":
                        return await FeedbackRequestHandler.HandleViewFeedbackForItem(parameters, feedbackService);

                    case "VIEW_FEEDBACK_EMPLOYEE":
                        return await FeedbackRequestHandler.HandleViewFeedbackByEmployee(parameters, feedbackService);

                    case "GET_RECOMMENDED_ITEMS":
                        return await requestHandler.GetTopMenuItemsByMealType(parameters);

                    case "ROLLOUT_CHOICES":
                        return await requestHandler.RolloutChoices(parameters);

                    case "VIEW_CHOICE_VOTING_RESULT":
                        return await requestHandler.GetVotingResults();

                    case "ROLLOUT_FINAL_MEAL":
                        return await requestHandler.RolloutFinalMeal(parameters);

                    case "CHANGE_AVAILABILITY":
                        return await MenuItemRequestHandler.ChangeAvailability(parameters, menuItemService);

                    case "VIEW_MONTHLY_REPORT":
                        return await requestHandler.ViewMonthlyReport();

                    case "VOTE_MENU_ITEM":
                        return await requestHandler.VoteMenuItems(parameters);

                    case "GET_ROLLOUT_CHOICES":
                        return await requestHandler.GetVotingResults();

                    case "GET_NOTIFICATIONS":
                        return await requestHandler.GetNotifications();

                    case "ADD_DETAILED_FEEDBACK":
                        return await requestHandler.AddDetailedFeedback(parameters);

                    case "GET_DISCARD_LIST":
                        return await requestHandler.GetDiscardItems();

                    case "REMOVE_DISCARD_ITEM":
                        return await requestHandler.RemoveDiscardItem(parameters);

                    case "VIEW_DETAILED_FEEDBACK_ITEM":
                        return await requestHandler.ViewDetailedFeedbackOfItem(parameters);

                    case "GET_DETAILED_FEEDBACK_ITEM":
                        return await requestHandler.RequestDetailedFeedbackFromUser(parameters);

                    case "SETUP_PROFILE":
                        return await requestHandler.SetupProfile(parameters);

                    case "GET_DISCARDITEM_NAME":
                        return await requestHandler.GetDiscardItemName(parameters);

                    case "GET_SORTED_ROLLOUT_MENU":
                        return await requestHandler.GetRollOutMenuSortedByPreferences(parameters);

                    default:
                        return $"Invalid request type: {requestType}";
                }
            }
            catch (Common.CustomExceptions.ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (ProfileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (InvalidChoiceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (AuthenticateException ex)
            {
                Console.WriteLine(ex.Message);
                return ResponseUtils.CreateExceptionJsonResponse(ex.Message);
            }
        }
    }
}
