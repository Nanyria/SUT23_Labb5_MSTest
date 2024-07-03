using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal partial class TransferMoney
    {
        /// <summary>
        /// Nathalee:
        /// Metod för att ta ut pengar från egna konton. 
        /// Förbättringsförslag från mig själv inkluderar: hitta ett smidigt sätt att läsa in användaren så att det 
        /// enkelt går att skicka tillbaka hen till menyn. 
        /// Ändra så det inte är accountnumber som används i sökfunktionen, se över om det är smidigast att lägga in en parameter i IAccounts
        /// eller att använda metoder i list för detta.
        /// Utökade failsafes för att säkerställa att det är rätt mängd pengar som tas ut.
        /// Lösenord när man tar ut?
        /// Ny metod i annan klass för att lagra informationen som skett i denna klass för att kunna komma åt historik.
        /// </summary>
        /// <param name="loggedInUser"></param>

        
        //public static void Withdraw(Customer loggedInCustomer)
        //{
        //    int maxPasswordAttempts = 3;

        //    while (true)
        //    {
        //        Console.Clear();
        //        ShowAllAcc(loggedInCustomer);

        //        Account sourceAccount = null;
        //        while (sourceAccount == null)
        //        {
        //            Console.WriteLine("Enter the number of the account you want to withdraw from:");
        //            if (!int.TryParse(Console.ReadLine(), out int withdrawFromOrder))
        //            {
        //                Console.WriteLine("Invalid input. Please enter a valid account number.");
        //                continue;
        //            }

        //            sourceAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == withdrawFromOrder);

        //            if (sourceAccount == null)
        //            {
        //                Console.WriteLine("Please enter a valid account number.");
        //            }
        //        }

        //        Console.WriteLine("Enter the amount to withdraw:");
        //        if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount) || withdrawAmount <= 0 || withdrawAmount > sourceAccount.Balance)
        //        {
        //            Console.WriteLine("Invalid withdraw amount.");
        //            return;
        //        }

        //        bool validChoice = false;

        //        while (!validChoice)
        //        {
        //            Console.WriteLine($"Confirm transaction (1 to confirm, 0 to cancel):");
        //            if (!int.TryParse(Console.ReadLine(), out int confirm) || (confirm != 0 && confirm != 1))
        //            {
        //                Console.WriteLine("Invalid confirmation input. Transaction cancelled.");
        //                Console.Clear();
        //                return;
        //            }


        //            Console.WriteLine("Enter Password to confirm withdrawal:");
        //            string enteredPassword = Validator.GetHiddenInput();
        //            while (loggedInCustomer.PasswordAttempts > maxPasswordAttempts)
        //            {
        //                if (ValidatePassForWithdraw(loggedInCustomer.UserName, enteredPassword))
        //                {
                            
        //                    sourceAccount.Balance -= withdrawAmount;

        //                    Transaction transaction = new Transaction
        //                    {
        //                        TransactionType = "Withdraw money",
        //                        Amount = withdrawAmount,
        //                        Timestamp = DateTime.Now
        //                    };
        //                    loggedInCustomer.TransactionHistory.Add(transaction);
        //                    loggedInCustomer.PasswordAttempts = 0;


        //                    Console.WriteLine($"Withdrawal of {withdrawAmount} ({sourceAccount.Currency}) from account {sourceAccount.AccountNumber} successful.");
        //                    break;
        //                }
        //                else if (!ValidatePassForWithdraw(loggedInCustomer.UserName, enteredPassword) && loggedInCustomer.PasswordAttempts != 3)
        //                {
        //                    loggedInCustomer.PasswordAttempts++;

        //                    Console.WriteLine($"Incorrect password. You have {maxPasswordAttempts - loggedInCustomer.PasswordAttempts} attempts remaining."); //Inte klart
        //                    continue;
        //                }
        //                else if (!ValidatePassForWithdraw(loggedInCustomer.UserName, enteredPassword) && loggedInCustomer.PasswordAttempts == 3)
        //                {
        //                    LockOutUser(loggedInCustomer);
        //                    CustomerManager.LogOut(loggedInCustomer);
        //                }

        //            }

        //            validChoice = true;
        //        }

        //        Console.WriteLine("Press Enter to return to account choice or any other key to exit.");
        //        if (Console.ReadKey().Key != ConsoleKey.Enter)
        //        {
        //            break; // Exit the loop if any key other than Enter is pressed

        //        }
        //    }

        //    Console.WriteLine("Press Enter to return to the menu.");
        //    Console.ReadLine();
        //    Console.Clear();
        //}


        ///// <summary>
        ///// Nathalee:
        ///// Metod för att sätta in pengar på egna konton. 
        ///// Förbättringsförslag från mig själv inkluderar: 
        ///// !Hitta ett smidigt sätt att läsa in användaren så att det 
        ///// enkelt går att skicka tillbaka hen till menyn. 
        ///// Ändra så det inte är accountnumber som används i sökfunktionen, se över om det är smidigast att lägga in en parameter i IAccounts
        ///// eller att använda metoder i list för detta.
        ///// Utökade failsafes för att säkerställa att det är rätt mängd pengar som förs ut, kanske sätta fasta summor? (Tänker tex om man ska utgå från sedlar).
        ///// Tror det går att göra intressanta saker framöver där. 
        ///// Ny metod i annan klass för att lagra informationen som skett i denna klass för att kunna komma åt historik.
        ///// </summary>
        ///// <param name="loggedInUser"></param>

        //public static void Deposit(Customer loggedInCustomer)
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        ShowAllAcc(loggedInCustomer);
        //        Console.WriteLine("Enter the number of the account you want to deposit to:");
        //        if (!int.TryParse(Console.ReadLine(), out int depositToOrder))
        //        {
        //            Console.WriteLine("Invalid input. Please enter a valid account number.");
        //            continue;
        //        }
        //        Account depositTo = loggedInCustomer.Accounts.Find(a => a.SortOrder == depositToOrder);
        //        if (depositTo == null)
        //        {
        //            Console.WriteLine("Destination account not found.");
        //            continue;
        //        }

        //        string depositCurrency = Converter.GetValidChoice(); // User choose currency.
        //        if (depositCurrency == null)
        //            continue;
        //        Console.WriteLine("Enter the amount to deposit:");
        //        if (!decimal.TryParse(Console.ReadLine(), out decimal depositAmount) || depositAmount <= 0)
        //        {
        //            Console.WriteLine("Invalid deposit amount.");
        //            continue;
        //        }
        //        decimal convertedAmount = Converter.ConvertDepositMoney(depositCurrency, depositTo, depositAmount); // Added this line.
        //        Console.WriteLine($"{depositAmount} {depositCurrency} will become {convertedAmount} {depositTo.Currency}, proceed?\n\n Press Enter to return to account choice.");

        //        Console.WriteLine($"Confirm transaction (1 to confirm, 0 to cancel):");
        //        if (!int.TryParse(Console.ReadLine(), out int confirm) || (confirm != 0 && confirm != 1))
        //        {
        //            Console.WriteLine("Invalid confirmation input. Transaction cancelled.");
        //            Console.Clear();
        //            return;
        //        }
        //        if (confirm == 1)
        //        {
        //            depositTo.Balance += convertedAmount;

        //            Transaction transaction = new Transaction
        //            {
        //                TransactionType = "Deposit money",
        //                Amount = depositAmount,
        //                Timestamp = DateTime.Now
        //            };
        //            loggedInCustomer.TransactionHistory.Add(transaction);

        //            Console.WriteLine($"Deposit of {depositAmount} ({depositCurrency}) to account {depositTo.AccountNumber}  successful.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Transaction cancelled.");
        //        }

        //        Console.WriteLine("Press Enter to return to menu");
        //        if (Console.ReadKey().Key != ConsoleKey.Enter)
        //        {
        //            break;
        //        }

        //        Console.Clear();
        //        CustomerManager.Menu(loggedInCustomer);
        //        return;
        //    }
        //}

        //private static bool ValidatePassForWithdraw(string enteredName, string enteredPassword)
        //{

        //    Customer storedUser = DataManager.customerList[enteredName];
        //    if (enteredPassword == storedUser.PassWord)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //}
        //private static void IncrementPasswordAttempts(Customer loggedInCustomer)
        //{
        //    //if (loggedInCustomer.PasswordAttempts != 3)
        //    //{
        //    //    loggedInCustomer.PasswordAttempts++;
        //    //}
        //    //else if (loggedInCustomer.PasswordAttempts == 3)
        //    //{
        //    //    LockOutUser(loggedInCustomer);
        //    //    CustomerManager.LogOut(loggedInCustomer);
        //    //}
        //}
        //private static void LockOutUser(Customer loggedInCustomer)
        //{
        //    CustomerLogin.loginAttemptsCount.Add(loggedInCustomer.UserName, loggedInCustomer.PasswordAttempts);
        //    Console.WriteLine($"User {loggedInCustomer.UserName} is locked out. Please contact support.");
        //}

    }


    //    public static void Deposit(Customer loggedInCustomer)
    //    {
    //        while (true)
    //        {
    //            Console.Clear();
    //            ShowAllAcc(loggedInCustomer);

    //            Console.WriteLine("Enter the number of the account you want to deposit to:");
    //            if (!int.TryParse(Console.ReadLine(), out int transferFromOrder))
    //            {
    //                Console.WriteLine("Invalid input. Please enter a valid account number.");
    //                continue;
    //            }

    //            Account depositTo = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferFromOrder);

    //            if (depositTo == null)
    //            {
    //                Console.WriteLine("Source account not found.");
    //                continue;
    //            }

    //            Console.WriteLine("Enter the amount to deposit:");
    //            if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount) || transferAmount <= 0 || transferAmount > depositTo.Balance)
    //            {
    //                Console.WriteLine("Invalid transfer amount.");
    //                continue;
    //            }

    //            //decimal money = Converter.ConvertMoney(depositTo, targetAccount, transferAmount); //Behöver ny metod i convert

    //            //Console.WriteLine($"{transferAmount} {depositTo.Currency} will become {money} {targetAccount.Currency}, proceed?\n\n Press Enter to return to account choice.");
    //            //Console.WriteLine("[1]. Yes");
    //            //Console.WriteLine("[2]. No");

    //            if (int.TryParse(Console.ReadLine(), out int confirm) && confirm == 1)
    //            {
    //                depositTo.Balance += transferAmount;

    //                Transaction transaction = new Transaction
    //                {
    //                    TransactionType = "Deposit money",
    //                    Amount = transferAmount,
    //                    Timestamp = DateTime.Now
    //                };
    //                loggedInCustomer.TransactionHistory.Add(transaction);

    //                Console.WriteLine("Transaction successful."); //Mer info=
    //            }
    //            else
    //            {
    //                Console.WriteLine("Transaction cancelled.");
    //            }

    //            Console.WriteLine("Press Enter to return to account choice or any other key to exit.");
    //            if (Console.ReadKey().Key != ConsoleKey.Enter)
    //            {
    //                break; // Exit the loop if any key other than Enter is pressed
    //            }
    //        }

    //        Console.Clear();
    //        CustomerManager.Menu(loggedInCustomer);



    //    }

    //}

}

