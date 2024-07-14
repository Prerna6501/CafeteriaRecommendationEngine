namespace Common.Helpers
{
    public static class Helpers
    {
        public static string ReadString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public static bool ReadBool(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                if (bool.TryParse(Console.ReadLine(), out bool result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
            }
        }

        public static int ReadValidChoice(int min, int max, string message)
        {
            int choice;
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("Invalid choice, try again...");
            }
        }

        public static bool ReadYesNo(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "yes")
                {
                    return true;
                }
                else if (input == "no")
                {
                    return false;
                }
                Console.WriteLine("Invalid input. Please enter 'Yes' or 'No'.");
            }
        }
    }
}

