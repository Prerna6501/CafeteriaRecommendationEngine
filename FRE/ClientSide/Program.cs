using Azure;
using Common.Enums;
using Newtonsoft.Json;
using ServerSide.Entity;

namespace ClientSide
{
    class Program
    {
        static async Task Main(string[] args)
        {       
            Console.Write("Enter User ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
                       
            string loginMessage = $"AUTHENTICATE_USER|{id},{name},{password}";
            string role = await AuthFunction.AuthenticateUser(loginMessage);
            if(role != null)
            {
                if(role == EnumExtensions.GetDescription(UserTypeEnum.Employee))
                {
                    await FunctionalityMenu.EmployeeFunctionality(int.Parse(id));
                }
                else if(role == EnumExtensions.GetDescription(UserTypeEnum.Chef))
                {
                    await FunctionalityMenu.ChefFunctionality();
                }
                else if(role == EnumExtensions.GetDescription(UserTypeEnum.Admin))
                {
                    await FunctionalityMenu.AdminFunctionality();
                }
                else
                {
                    Console.WriteLine("Invalid role");
                }
            }
        }
    }
}

