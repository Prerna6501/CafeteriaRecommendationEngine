using Common.Models;
using ConsoleTables;
using Newtonsoft.Json;
using ServerSide.Entity;

namespace ClientSide
{
    class FunctionalityMenu
    {
        public static async Task AdminFunctionality()
        {
            while (true)
            {
                Console.WriteLine("Admin Funtionality /n Please select :");
                Console.WriteLine("1.View MenuItem \n2.Create MenuItems \n3.Update Menultems \n4.Delete MenuItems \n5.Exit\n");
                string input = Console.ReadLine();
                bool IsValidChoice = int.TryParse(input, out int choice);
                if (!IsValidChoice || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Wrong choice, try again...\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await ViewMenuItems();
                        PrintSeparatorLine();
                        break;
                    case 2:
                        await CreateMenuItem();
                        PrintSeparatorLine();
                        break;
                    case 3:
                        await UpdateMenuItem();
                        PrintSeparatorLine();
                        break;
                    case 4:
                        await DeleteMenuItem();
                        PrintSeparatorLine();
                        break;
                    case 5:
                        Console.WriteLine("Logout");
                        return;
                    default:
                        break;
                }
            }
        }

        public static async Task ChefFunctionality()
        {
            while (true)
            {
                Console.WriteLine("Chef Functionality\nPlease select:");
                Console.WriteLine("1. Get Recommended Items\n2. Rollout Choices\n3. View Choice Voting Result\n4. Give Final Menu\n5. Change Availability\n6. View Feedback for a particular item\n7. View Monthly Report\n8. Exit\n");
                string input = Console.ReadLine();
                bool isValidChoice = int.TryParse(input, out int choice);
                if (!isValidChoice || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Wrong choice, try again...\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await GetRecommendedItems();
                        PrintSeparatorLine();
                        break;
                    case 2:
                        await RolloutChoices();
                        PrintSeparatorLine();
                        break;
                    case 3:
                        await ViewChoiceVotingResult();
                        PrintSeparatorLine();
                        break;
                    case 4:
                        await RolloutFinalMeal();
                        PrintSeparatorLine();
                        break;
                    case 5:
                        await ChangeAvailability();
                        PrintSeparatorLine();
                        break;
                    case 6:
                        await ViewFeedbackForItem();
                        PrintSeparatorLine();
                        break;
                    case 7:
                        await ViewMonthlyReport();//OPTIONAL
                        PrintSeparatorLine();
                        break;
                    case 8:
                        Console.WriteLine("Logout");
                        return;
                    default:
                        break;
                }
            }
        }

        public static async Task EmployeeFunctionality(int userId)
        {
            while (true)
            {                
                Console.WriteLine("Employee Functionality\nPlease select:");
                Console.WriteLine("1. View MenuItem\n2. Give Feedback\n3. View Feedback for a particular item\n4. View all feedback given by you\n5. Get Rolled out menu \n");
                string input = Console.ReadLine();
                bool isValidChoice = int.TryParse(input, out int choice);
                if (!isValidChoice || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Wrong choice, try again...\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await ViewMenuItems();
                        PrintSeparatorLine();
                        break;
                    case 2:
                        await GiveFeedback(userId);
                        PrintSeparatorLine();
                        break;
                    case 3:
                        await ViewFeedbackForItem();
                        PrintSeparatorLine();
                        break;
                    case 4:
                        await ViewAllFeedbackByEmployee(userId);
                        PrintSeparatorLine();
                        break;
                    case 5:
                        await GetRolloutChoices();
                        PrintSeparatorLine();
                        break;
                    case 6:
                        await VoteForMenuItems();
                        PrintSeparatorLine();
                        break;
                    case 7:
                        Console.WriteLine("Logout");
                        PrintSeparatorLine();
                        return;
                    default:
                        break;
                }
            }
        }

        private static async void PrintSeparatorLine()
        {
            Console.WriteLine("===========================================================================================================================================");
        }

        private static async Task GetRolloutChoices()
        {
            string request = "GET_ROLLOUT_CHOICES|";
            string response = await HandleRequest.SendRequest(request);
            List<VotingResultModel> votingResults = JsonConvert.DeserializeObject<List<VotingResultModel>>(response);
            var table = new ConsoleTable("MenuItem Id", "MenuItem Name", "MealType");
            foreach (var item in votingResults)
            {
                table.AddRow(item.MenuItemId, item.MenuItemName, item.MealType);
            }
            table.Write(Format.Alternative);
            }
        }

        private static async Task GiveFeedback(int userId)
        {
            Console.WriteLine("Enter MenuItem ID to give feedback:");
            string itemId = Console.ReadLine();
            Console.WriteLine("Enter your rating (1-5):");
            string rating = Console.ReadLine();
            Console.WriteLine("Enter your comments:");
            string comments = Console.ReadLine();

            string request = $"GIVE_FEEDBACK|{itemId},{rating},{comments},{userId}";
            string response = await HandleRequest.SendRequest(request);
            Console.WriteLine(response);
        }

        private static async Task ViewFeedbackForItem()
        {
            Console.WriteLine("Enter MenuItem ID to view feedback:");
            string itemId = Console.ReadLine();

            string request = $"VIEW_FEEDBACK_ITEM|{itemId}";
            string response = await HandleRequest.SendRequest(request);

            Console.WriteLine("Feedback for the item:\n");

            List<FeedbackModel> feedbacks = JsonConvert.DeserializeObject<List<FeedbackModel>>(response);
            var table = new ConsoleTable( "FeedBack Id", "MenuItem Id", "Rating", "Comments");
            foreach (var item in feedbacks)
            {
                table.AddRow(item.Id, item.MenuItemId, item.Rating, item.Comment);
            }
            table.Write(Format.Alternative);
        }

        private static async Task ViewAllFeedbackByEmployee(int userId)
        {
            string request = $"VIEW_FEEDBACK_EMPLOYEE|{userId}";
            string response = await HandleRequest.SendRequest(request);

            Console.WriteLine("All feedback given by you:\n");
            List<FeedbackModel> feedbacks = JsonConvert.DeserializeObject<List<FeedbackModel>>(response);
            var table = new ConsoleTable("FeedBack Id", "MenuItem Id","MenuItem Name", "Rating", "Comments");
            foreach (var item in feedbacks)
            {
                table.AddRow(item.Id, item.MenuItemId,item.MenuItemName, item.Rating, item.Comment);
            }
            table.Write(Format.Alternative);
        }

        private static async Task GetRecommendedItems()
        {
            Console.WriteLine("Enter Mealtype Recommendation you want to see: \n1.Breakfast \n2.Lunch \n3.Dinner");
            string mealtypeId = Console.ReadLine();
            Console.WriteLine("\nNo of recommendation you need:");
            string topN = Console.ReadLine();
            string request = $"GET_RECOMMENDED_ITEMS|{mealtypeId},{topN}";
            string response = await HandleRequest.SendRequest(request);
            List<MenuItemModel> menuItemModel = JsonConvert.DeserializeObject<List<MenuItemModel>>(response);
            var table = new ConsoleTable("Id", "Name", "Sentiment", "Average Rating");
            foreach (var item in menuItemModel)
            {
                table.AddRow(item.Id, item.Name, item.Sentiments, item.AverageRating);
            }
            table.Write(Format.Alternative);
        }

        private static async Task RolloutChoices()
        {
            Console.WriteLine("Enter the Roll out menu for breakfast (comma-separated):");
            string breakFastOptions = Console.ReadLine();
            Console.WriteLine("Enter the Roll out menu for lunch (comma-separated):");
            string lunchOptions = Console.ReadLine();
            Console.WriteLine("Enter the Roll out menu for dinner (comma-separated):");
            string dinnerOptions = Console.ReadLine();

            string request = $"ROLLOUT_CHOICES|Breakfast:{breakFastOptions};Lunch:{lunchOptions};Dinner:{dinnerOptions}";
            string response = await HandleRequest.SendRequest(request);

            Console.WriteLine(response);
        }

        private static async Task ViewChoiceVotingResult()
        {
            string request = "VIEW_CHOICE_VOTING_RESULT|";
            string response = await HandleRequest.SendRequest(request);
            List<VotingResultModel> votingResults = JsonConvert.DeserializeObject<List<VotingResultModel>>(response);
            var table = new ConsoleTable("Menuitem Id", "MenuItem Name", "MealType", "Votes");
            foreach (var item in votingResults)
            {
                table.AddRow(item.MenuItemId, item.MenuItemName, item.MealType, item.Votes);
            }
            table.Write(Format.Alternative);
        }

        private static async Task RolloutFinalMeal()
        {
            Console.WriteLine("Enter the Final menu for breakfast(1):");
            string breakFastOption = Console.ReadLine();
            Console.WriteLine("Enter the Final menu for lunch (2):");
            string lunchOptions = Console.ReadLine();
            Console.WriteLine("Enter the Final menu for dinner (2):");
            string dinnerOptions = Console.ReadLine();

            string request = $"ROLLOUT_FINAL_MEAL|Breakfast:{breakFastOption};Lunch:{lunchOptions};Dinner:{dinnerOptions}";
            string response = await HandleRequest.SendRequest(request);
            Console.WriteLine(response);
        }

        private static async Task ChangeAvailability()
        {
            Console.WriteLine("Enter MenuItem ID to change availability:");
            string itemId = Console.ReadLine();
            Console.WriteLine("Enter availability status (true/false):");
            string availabilityStatus = Console.ReadLine();

            string request = $"CHANGE_AVAILABILITY|{itemId},{availabilityStatus}";
            string response = await HandleRequest.SendRequest(request);
            Console.WriteLine(response);
        }


        private static async Task ViewMonthlyReport()
        {
            string request = "VIEW_MONTHLY_REPORT|";
            string response = await HandleRequest.SendRequest(request);

            Console.WriteLine("Monthly report:");
            Console.WriteLine(response);
        }

        private static async Task ViewMenuItems()
        {
            string response = await HandleRequest.SendRequest("VIEW_MENU|");
            Console.WriteLine("List of MenuItems:");
            if (response != null)
            {
                List<MenuItem> menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(response);
                var table = new ConsoleTable("Id", "Name", "Price", "IsAvailable");
                foreach (var menuItem in menuItems)
                {
                    table.AddRow(menuItem.Id, menuItem.Name, menuItem.Price, menuItem.IsAvailable);
                }
                table.Write(Format.Alternative);
            }
            else
            {
                Console.WriteLine("NO MenuItem at the moment.");
            }
        }
        private static async Task CreateMenuItem()
        {
            Console.WriteLine("Enter MenuItem Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Price:");
            string price = Console.ReadLine();
            Console.WriteLine("Enter MenuItem Availability Status (true/false):");
            string availabilityStatus = Console.ReadLine();
            Console.WriteLine("Enter the MenuTypeId");
            string MenuItemTypeId = Console.ReadLine();

            string request = $"ADD_MENU_ITEM|{name},{price},{availabilityStatus},{MenuItemTypeId}";
            string response = await HandleRequest.SendRequest(request);
            MenuItem menuItem = JsonConvert.DeserializeObject<MenuItem>(response);
            var table = new ConsoleTable("Name", "Price", "MenuTypeId");
            table.AddRow(menuItem.Name, menuItem.Price, menuItem.MenuItemTypeId);
            table.Write(Format.Alternative);
        }

        private static async Task UpdateMenuItem()
        {
            Console.WriteLine("Enter MenuItem ID:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter MenuItem Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter MenuItem Price:");
            string price = Console.ReadLine();
            Console.WriteLine("Enter MenuItem Availability Status (true/false):");
            string availabilityStatus = Console.ReadLine();
            Console.WriteLine("Do you want to soft delete (true/false)");
            string softDelete = Console.ReadLine();
            Console.WriteLine("Enter the MenuTypeId");
            string MenuItemTypeId = Console.ReadLine();

            string request = $"UPDATE_MENU|{id},{name},{price},{availabilityStatus},{softDelete}, {MenuItemTypeId}";
            string response = await HandleRequest.SendRequest(request);

            MenuItem menuItem = JsonConvert.DeserializeObject<MenuItem>(response);
            var table = new ConsoleTable("Name", "Price");
            table.AddRow(menuItem.Name, menuItem.Price);
            table.Write(Format.Alternative);
        }

        private static async Task DeleteMenuItem()
        {
            Console.WriteLine("Enter MenuItem ID:");
            string id = Console.ReadLine();

            string request = $"DELETE_MENU_ITEM|{id}";
            string response = await HandleRequest.SendRequest(request);

            MenuItem menuItem = JsonConvert.DeserializeObject<MenuItem>(response);
            var table = new ConsoleTable("Name", "IsDeleted");
            table.AddRow(menuItem.Name, menuItem.IsDeleted);
            table.Write(Format.Alternative);
        }
    }
}




