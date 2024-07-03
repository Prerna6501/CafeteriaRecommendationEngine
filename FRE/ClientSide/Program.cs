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
            await FunctionalityMenu.GetFunctionalityMenu(role, id);
        }
    }
}

