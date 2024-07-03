using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class CustomerLogin 
    {
        /// <summary>
        /// Ändrat att personen loggas in som Customer (klass) samt att sökfunktionen utgår ifrån den nya dictionaryn customerList /N
        /// Ändrat så att klassen ärver från ICustomerLogin ist för ILogin. 
        /// 2023-12-15
        /// </summary>
        /// <returns></returns>
        /// 
        public static Dictionary<string, int> loginAttemptsCount = new Dictionary<string, int>();

        public static Customer Login()
        {
            AsciiHeadliner.PrintHeadliner();
            Customer loggedInCustomer = null;
            int maxLoginAttempts = 3;

            while (loggedInCustomer == null)
            {
                Console.WriteLine("Username:");
                string enteredName = Console.ReadLine();

                if (IsUserLockedOut(enteredName))
                {
                    Console.Clear();
                    Console.WriteLine("This account is locked. Please contact support.");
                    break;
                }

                if (DataManager.customerList.ContainsKey(enteredName))
                {
                    Console.WriteLine("Password:");
                    string enteredPassword = Validator.GetHiddenInput();

                    if (ValidateCustomerPassword(enteredName, enteredPassword))
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome, " + enteredName + "!");
                        loggedInCustomer = DataManager.customerList[enteredName];
                        loggedInCustomer.PasswordAttempts = 0;
                        CustomerManager.Menu(loggedInCustomer);
                    }
                    else
                    {
                        Console.Clear();
                        AsciiHeadliner.PrintHeadliner();
                        IncrementLoginAttempts(enteredName);
                        Console.WriteLine($"Incorrect password. You have {maxLoginAttempts - loginAttemptsCount[enteredName]} attempts remaining.");
                    }
                }
                else
                {
                    Console.Clear();
                    AsciiHeadliner.PrintHeadliner();
                    Console.WriteLine("Username not found.");
                }
            }

            
            return loggedInCustomer; //Överflödigt? 
            
        }


        /// <summary>
        /// Method to validate customer password.
        /// Ändrat att sökfunktionen utgår ifrån den nya dictionaryn customerList /N
        /// 2023-12-15
        /// </summary>
        /// <param name="enteredName"></param>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool ValidateCustomerPassword(string enteredName, string enteredPassword)

        {
            if (DataManager.customerList.ContainsKey(enteredName))
            {
                Customer storedUser = DataManager.customerList[enteredName];
                return enteredPassword == storedUser.PassWord;
            }
            else
            {
                throw new ArgumentException("User ID not found.");
            }
        }

        private static void IncrementLoginAttempts(string username)
        {
            if (!loginAttemptsCount.ContainsKey(username))
            {
                loginAttemptsCount[username] = 1;
            }
            else
            {
                loginAttemptsCount[username]++;
            }

            if (loginAttemptsCount[username] >= 3)
            {
                LockOutUser(username);
            }
        }

        private static bool IsUserLockedOut(string username)
        {
            return loginAttemptsCount.ContainsKey(username) && loginAttemptsCount[username] >= 3;
        }

        private static void LockOutUser(string username)
        {
            Console.WriteLine($"User {username} is locked out. Please contact support.");
        }
    }
    //public Customer Login()
    //    {
    //        AsciiHeadliner.PrintHeadliner();
    //        Customer loggedInCustomer = null;
    //        int loginAttempts = 0;
    //        int maxLoginAttempts = 3; // Assuming a maximum of 3 login attempts

    //        while (loginAttempts < maxLoginAttempts && loggedInCustomer == null)
    //        {
    //            Console.WriteLine("Username:");
    //            string enteredName = Console.ReadLine();
    //            // Validate if the entered username exists in CustomerUsers dictionary
    //            if (DataManager.customerList.ContainsKey(enteredName))
    //            {
    //                Console.WriteLine("Password:");
    //                string enteredPassword = Validator.GetHiddenInput();
    //                // Perform password validation here
    //                if (ValidateCustomerPassword(enteredName, enteredPassword)) // Example password validation
    //                {
    //                    Console.Clear();
    //                    Console.WriteLine("Welcome, " + enteredName + "!");
    //                    loggedInCustomer = DataManager.customerList[enteredName];
    //                    CustomerManager.Menu(loggedInCustomer);
    //                }
    //                else
    //                {
    //                    loginAttempts++;
    //                    Console.WriteLine($"Incorrect password. You have {maxLoginAttempts - loginAttempts} attempts remaining.");
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("Username not found.");
    //            }
    //        }

    //        if (loggedInCustomer == null)
    //        {
    //            Console.WriteLine("Maximum login attempts reached. Please contact support.");
    //        }

    //        return loggedInCustomer;

    //    }

    //    /// <summary>
    //    /// Method to validate customer password.
    //    /// Ändrat att sökfunktionen utgår ifrån den nya dictionaryn customerList /N
    //    /// 2023-12-15
    //    /// </summary>
    //    /// <param name="enteredName"></param>
    //    /// <param name="enteredPassword"></param>
    //    /// <returns></returns>
    //    /// <exception cref="ArgumentException"></exception>
    //    private bool ValidateCustomerPassword(string enteredName, string enteredPassword)
    //    {
    //        // Check if the userID exists in the dictionary
    //        if (DataManager.customerList.ContainsKey(enteredName))
    //        {
    //            // Retrieve the stored password corresponding to the userID
    //            Customer storedUser = DataManager.customerList[enteredName];
    //            return enteredPassword == storedUser.PassWord;
    //        }
    //        else
    //        {
    //            // UserID not found, handle the case (throw an exception, return false, etc.)
    //            // For example:
    //            throw new ArgumentException("User ID not found.");

    //        }
    //    }
    
}
