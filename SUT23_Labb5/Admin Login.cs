using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    /// <summary>
    /// Ändrat alla referenser från User till Admin samt ändrat userList till adminList. 
    /// Även ändrat så att den ärver av interface IAdminLogin istället. /N
    /// 2023-12-15
    /// </summary>
    internal class AdminLogin : IAdminLogin
    {

        private static Dictionary<string, int> loginAttemptsCount = new Dictionary<string, int>();

        public Admin Login()
        {
            AsciiHeadliner.PrintHeadliner();
            Admin loggedInAdmin = null;
            int maxLoginAttempts = 3;

            while (loggedInAdmin == null)
            {
                Console.WriteLine("Username:");
                string enteredName = Console.ReadLine();

                if (IsUserLockedOut(enteredName))
                {
                    Console.Clear();
                    Console.WriteLine("This account is locked. Please contact support.");
                    break;
                }

                if (DataManager.adminList.ContainsKey(enteredName))
                {
                    Console.WriteLine("Password:");
                    string enteredPassword = Validator.GetHiddenInput();

                    if (ValidateAdminPassword(enteredName, enteredPassword))
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome, " + enteredName + "!");
                        loggedInAdmin = DataManager.adminList[enteredName];
                        AdminManager.Menu(loggedInAdmin);
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

            return loggedInAdmin;
        }

        private bool ValidateAdminPassword(string enteredName, string enteredPassword)
        {
            if (DataManager.adminList.ContainsKey(enteredName))
            {
                Admin storedUser = DataManager.adminList[enteredName];
                return enteredPassword == storedUser.PassWord;
            }
            else
            {
                throw new ArgumentException("User ID not found.");
            }
        }

        private void IncrementLoginAttempts(string username)
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

        private bool IsUserLockedOut(string username)
        {
            return loginAttemptsCount.ContainsKey(username) && loginAttemptsCount[username] >= 3;
        }

        private void LockOutUser(string username)
        {
            Console.WriteLine($"User {username} is locked out. Please contact support.");
        }
    }
    //public Admin Login()
    //{
    //    AsciiHeadliner.PrintHeadliner();
    //    Admin loggedInAdmin = null;
    //    int loginAttempts = 0;
    //    int maxLoginAttempts = 3; // Assuming a maximum of 3 login attempts

    //    while (loginAttempts < maxLoginAttempts && loggedInAdmin == null)
    //    {
    //        Console.WriteLine("Username:");
    //        string enteredName = Console.ReadLine();
    //        // Validate if the entered username exists in CustomerUsers dictionary
    //        if (DataManager.adminList.ContainsKey(enteredName))
    //        {
    //            Console.WriteLine("Password:");
    //            string enteredPassword = Validator.GetHiddenInput();
    //            // Perform password validation here
    //            if (ValidateAdminPassword(enteredName, enteredPassword)) // Example password validation
    //            {
    //                Console.Clear();
    //                Console.WriteLine("Welcome, " + enteredName + "!");
    //                loggedInAdmin = DataManager.adminList[enteredName];
    //                AdminManager.Menu(loggedInAdmin);
    //            }
    //            else
    //            {
    //                Console.Clear();
    //                AsciiHeadliner.PrintHeadliner();
    //                loginAttempts++;
    //                Console.WriteLine($"Incorrect password. You have {maxLoginAttempts - loginAttempts} attempts remaining.");
    //            }
    //        }
    //        else
    //        {
    //            Console.Clear();
    //            AsciiHeadliner.PrintHeadliner();
    //            Console.WriteLine("Username not found.");
    //        }
    //    }

    //    if (loggedInAdmin == null)
    //    {
    //        Console.WriteLine("Maximum login attempts reached. Please contact support.");
    //    }

    //    return loggedInAdmin;

    //}

    //// Method to validate admin password
    //private bool ValidateAdminPassword(string enteredName, string enteredPassword)
    //{
    //    // Check if the userID exists in the dictionary
    //    if (DataManager.adminList.ContainsKey(enteredName))
    //    {
    //        // Retrieve the stored password corresponding to the userID
    //        Admin storedUser = DataManager.adminList[enteredName];
    //        return enteredPassword == storedUser.PassWord;
    //    }
    //    else
    //    {
    //        // UserID not found, handle the case (throw an exception, return false, etc.)
    //        // For example:
    //        throw new ArgumentException("User ID not found.");

    //    }

    //}
}

