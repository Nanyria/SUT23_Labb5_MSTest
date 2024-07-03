using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class ShowAllUserInfo
    {
        public static void ShowAllInfo(Admin loggedInAdmin)
        {
            bool go = true;
            while (go)
            {
                try
                {
                    Console.WriteLine("Type of user account:");
                    Console.WriteLine("1. Admin.");
                    Console.WriteLine("2. Customer.");
                    Console.Write("3. Back to Menu.");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            ShowAllUserInfo.ShowAdminInfo();
                            break;
                        case 2:
                            Console.Clear();
                            ShowAllUserInfo.ShowCustomerInfo();
                            break;
                        case 3:
                            Console.Clear();
                            go = false; // Exit the inner loop
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Insert number between 1-3.");
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a Number.");
                }
            }
        }
        public static void ShowAdminInfo()
        {
            // Show a numbered list of admin usernames for selection
            int adminNumber = 1;
            foreach (KeyValuePair<string, Admin> admin in DataManager.adminList)
            {


                Console.WriteLine($"{adminNumber}. {admin.Key} {admin.Value.FirstName} {admin.Value.LastName}");
                adminNumber++;

            }
            Console.WriteLine("Press any key to Exit.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ShowCustomerInfo()
        {
            Console.WriteLine("Choose a customer to see more information:");

            // Show a numbered list of customer usernames for selection
            int customerNumber = 1;
            foreach (KeyValuePair<string, Customer> customer in DataManager.customerList)
            {


                Console.WriteLine($"{customerNumber}. {customer.Value.UserName} {customer.Value.FirstName} {customer.Value.LastName}");
                customerNumber++;

            }

            // Allow the user to choose a customer by entering the corresponding number
            int selectedCustomerNumber = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            // Display detailed information for the selected customer
            int currentCustomerNumber = 1;
            foreach (KeyValuePair<string, Customer> user in DataManager.customerList)
            {
                if (user.Value is Customer customer)
                {
                    if (currentCustomerNumber == selectedCustomerNumber)
                    {
                        Console.WriteLine($"Customer UserName: {customer.UserName}");
                        Console.WriteLine($"Customer FirstName: {customer.FirstName}");
                        Console.WriteLine($"Customer LastName: {customer.LastName}");

                        Console.WriteLine("Accounts:");
                        if (customer.Accounts != null)
                        {
                            foreach (Account account in customer.Accounts)
                            {
                                Console.WriteLine($"Account Number: {account.AccountNumber}");
                                Console.WriteLine($"Account Name: {account.AccountName}"); //Förlag - byt plats på name och number så att accname står överst /N
                                Console.WriteLine($"Balance: {account.Balance}");
                                Console.WriteLine($"Currency: {account.Currency}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No accounts for this user.");
                        }
                        Console.WriteLine("---------------------------");

                        Console.WriteLine("Press any key to Exit.");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    currentCustomerNumber++;
                }
            }
        }
    }

        //public static void ShowAllInfo()
        //{
        //    bool go = true;

        //    while (go)
        //    {
        //        Console.WriteLine("Type of user account:");
        //        Console.WriteLine("1. Admin.");
        //        Console.WriteLine("2. Customer.");
        //        Console.WriteLine("3. Exit to main menu.");
        //        int choice = Convert.ToInt32(Console.ReadLine());
        //        Console.Clear();

        //        switch (choice)
        //        {
        //            case 1:
        //                ShowAdminInfo();
        //                break;

        //            case 2:
        //                ShowCustomerInfo();
        //                break;

        //            case 3:
        //                go = false;
        //                break;

        //            default:
        //                Console.WriteLine("Invalid choice. Please enter '1', '2' or '3'.");
        //                break;
        //        }
        //    }
        //}
        //public static void ShowAdminInfo()
        //{
        //    // Show a numbered list of admin usernames for selection
        //    int adminNumber = 1;
        //    foreach (KeyValuePair<string, User> user in DataManager.userList)
        //    {
        //        if (user.Value is Admin admin)
        //        {
        //            Console.WriteLine($"{adminNumber}. {admin.UserName} {admin.FirstName} {admin.LastName}");
        //            adminNumber++;
        //        }
        //    }
        //    Console.WriteLine("Press any key to Exit.");
        //    Console.ReadKey();
        //    Console.Clear();
        //}
    

    //    public static void ShowCustomerInfo()
    //    {
    //        Console.WriteLine("Choose a customer to see more information:");

    //        // Show a numbered list of customer usernames for selection
    //        int customerNumber = 1;
    //        foreach (KeyValuePair<string, User> user in DataManager.userList)
    //        {
    //            if (user.Value is Customer customer)
    //            {
    //                Console.WriteLine($"{customerNumber}. {customer.UserName} {customer.FirstName} {customer.LastName}");
    //                customerNumber++;
    //            }
    //        }

    //        // Allow the user to choose a customer by entering the corresponding number
    //        int selectedCustomerNumber = Convert.ToInt32(Console.ReadLine());
    //        Console.Clear();

    //        // Display detailed information for the selected customer
    //        int currentCustomerNumber = 1;
    //        foreach (KeyValuePair<string, User> user in DataManager.userList)
    //        {
    //            if (user.Value is Customer customer)
    //            {
    //                if (currentCustomerNumber == selectedCustomerNumber)
    //                {
    //                    Console.WriteLine($"Customer UserName: {customer.UserName}");
    //                    Console.WriteLine($"Customer FirstName: {customer.FirstName}");
    //                    Console.WriteLine($"Customer LastName: {customer.LastName}");

    //                    Console.WriteLine("Accounts:");
    //                    if (customer.Accounts != null)
    //                    {
    //                        foreach (Account account in customer.Accounts)
    //                        {
    //                            Console.WriteLine($"Account Number: {account.AccountNumber}");
    //                            Console.WriteLine($"Account Name: {account.AccountName}");
    //                            Console.WriteLine($"Balance: {account.Balance}");
    //                            Console.WriteLine($"Currency: {account.Currency}");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine("No accounts for this user.");
    //                    }
    //                    Console.WriteLine("---------------------------");

    //                    Console.WriteLine("Press any key to Exit.");
    //                    Console.ReadKey();
    //                    Console.Clear();
    //                }

    //                currentCustomerNumber++;
    //            }
    //        }
    //    }
    //}
}

