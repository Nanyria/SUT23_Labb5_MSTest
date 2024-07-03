using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class Validator
    {
        public static int GetValidInt()
        {
            int choice;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Invalid input. Please enter an integer.");
                else
                    return choice;
            }
        }
        public static int Get1Or2()
        {
            int choice;
            int max = 0;
            while (max < 5)
            {
                max++;
                if (!int.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine("Invalid input. Please enter an integer.");
                else if (choice != 1 && 2 != choice)
                {
                    Console.WriteLine("Insert 1 or 2");
                }
                else
                    return choice;
            }
            Console.Clear();
            Console.WriteLine("You tried 5 times. Go back to menu.");
            //CustomerManager.Meny();
            Console.ReadKey();
            return 0;

        }
        public static string GetValidString()
        {
            string choice = String.Empty;
            while (String.IsNullOrEmpty(choice))
            {
                choice = Console.ReadLine();
                if (String.IsNullOrEmpty(choice))
                    Console.WriteLine("Please insert");
            }
            return choice;
        }
        public static string GetHiddenInput()
        {
            StringBuilder input = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after hiding the password

            return input.ToString();
        }

        public static int GetValidIntOrMenu(Customer loggedInCustomer) // This goes back to meny if the user pressed enter.
        {
            string input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                CustomerManager.Menu(loggedInCustomer);
                return -1;
            }
            else
            {
                if (int.TryParse(input, out int number))
                    return number;
                else
                {
                    Console.WriteLine("Invalid input. Please enter integer.");
                    return GetValidIntOrMenu(loggedInCustomer);
                }
            }
        }
        public static decimal GetValidDecimal()
        {
            decimal money;
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine(), out money))
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
                else
                {
                    return money;
                }
            }
        }
        internal static int GetValidInt(string prompt, int minValue, int maxValue)
        {
            int userInput;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput >= minValue && userInput <= maxValue)
                    {
                        return userInput;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between {minValue} and {maxValue}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

        }
        public static string GetValidName(string prompt)
        {
            string name;
            while (true)
            {
                Console.Write(prompt);
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    return name;
                }

                Console.WriteLine("Invalid input. Please enter a valid name with only letters.");
            }
        }
        public static string GetValidPassword()
        {
            while (true)
            {
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (IsPasswordValid(password))
                {
                    return password;
                }

                Console.WriteLine("Invalid password. Please enter a valid password.");
            }
        }

        public static bool IsPasswordValid(string password)
        {
            // Krav: Mellan 6-12 tecken, minst en stor bokstav, minst en siffra, minst en symbol
            return password.Length >= 6 && password.Length <= 12
                && password.Any(char.IsUpper)
                && password.Any(char.IsDigit)
                && password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
    //internal class Validator
    //{
    //    public static int GetValidInt()
    //    {
    //        int choice;
    //        while (true)
    //        {
    //            if (!int.TryParse(Console.ReadLine(), out choice))
    //                Console.WriteLine("Invalid input. Please enter an integer.");
    //            else
    //                return choice;
    //        }
    //    }
    //    public static int Get1Or2()
    //    {
    //        int choice;
    //        int max = 0;
    //        while (max < 5)
    //        {
    //            max++;
    //            if (!int.TryParse(Console.ReadLine(), out choice))
    //                Console.WriteLine("Invalid input. Please enter an integer.");
    //            else if (choice != 1 && 2 != choice)
    //            {
    //                Console.WriteLine("Insert 1 or 2");
    //            }
    //            else
    //                return choice;
    //        }
    //        Console.Clear();
    //        Console.WriteLine("You tried 5 times. Go back to menu.");
    //        //CustomerManager.Meny();
    //        Console.ReadKey();
    //        return 0;

    //    }
    //    public static string GetValidString()
    //    {
    //        string choice = String.Empty;
    //        while (String.IsNullOrEmpty(choice))
    //        {
    //            choice = Console.ReadLine();
    //            if (String.IsNullOrEmpty(choice))
    //                Console.WriteLine("Please insert");
    //        }
    //        return choice;
    //    }

    //    public static int GetValidIntOrMenu(User user) // This goes back to meny if the user pressed enter.
    //    {
    //        CustomerManager cus = new CustomerManager();
    //        string input = Console.ReadLine();
    //        if (String.IsNullOrEmpty(input))
    //        {
    //            cus.Meny(user);
    //            return -1;
    //        }
    //        else
    //        {
    //            if (int.TryParse(input, out int number))
    //                return number;
    //            else
    //            {
    //                Console.WriteLine("Invalid input. Please enter integer.");
    //                return GetValidIntOrMenu(user);
    //            }
    //        }
    //    }
    //    public static decimal GetValidDecimal()
    //    {
    //        decimal money;
    //        while (true)
    //        {
    //            if (!decimal.TryParse(Console.ReadLine(), out money))
    //            {
    //                Console.WriteLine("Invalid input. Please enter an integer.");
    //            }
    //            else
    //            {
    //                return money;
    //            }
    //        }
    //    }
    //    internal static int GetValidInt(string prompt, int minValue, int maxValue)
    //    {
    //        int userInput;

    //        while (true)
    //        {
    //            Console.Write(prompt);

    //            if (int.TryParse(Console.ReadLine(), out userInput))
    //            {
    //                if (userInput >= minValue && userInput <= maxValue)
    //                {
    //                    return userInput;
    //                }
    //                else
    //                {
    //                    Console.WriteLine($"Please enter a number between {minValue} and {maxValue}.");
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("Invalid input. Please enter a valid number.");
    //            }
    //        }
    //    }
    //}
}
