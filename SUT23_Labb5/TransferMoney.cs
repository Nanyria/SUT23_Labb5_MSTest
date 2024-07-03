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
        /// Metod för att föra över pengar till andra egna konton. 
        /// Ändra så det inte är accountnumber som används i sökfunktionen, se över om det är smidigast att lägga in en parameter i IAccounts
        /// eller att använda metoder i list för detta.
        /// Utökade failsafes för att säkerställa att det är rätt mängd pengar som förs över.
        /// Ny metod i annan klass för att lagra informationen som skett i denna klass för att kunna komma åt historik.
        /// </summary>
        /// <param name="loggedInUser"></param>
        public static void TransferMoneyBetweenAccount(Customer loggedInCustomer) //lägg till felmeddelande om inte accnr hittas i val
        {
            int transferFromOrder;
            int transferToOrder;
            decimal transferAmount;

            while (true)
            {
                Console.Clear();
                ShowAllAcc(loggedInCustomer);

                Console.WriteLine("Enter the number of the account you want to transfer from or press Enter to return:");
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    break; // Exit the loop if Enter is pressed to return to the menu
                }
                else if (!int.TryParse(key.KeyChar.ToString(), out transferFromOrder))
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                    continue;
                }

                Account sourceAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferFromOrder);

                if (sourceAccount == null)
                {
                    Console.WriteLine("Source account not found.");
                    continue;
                }
                Console.WriteLine($"\n{transferFromOrder}.Transfering from {sourceAccount.AccountName}.");
                Console.WriteLine("Enter the amount to transfer or press Enter to return:");

                if (key.Key == ConsoleKey.Enter)
                {
                    break; // Exit the loop if Enter is pressed to return to the menu
                }
                else if (!decimal.TryParse(Console.ReadLine(), out transferAmount) || transferAmount <= 0 || transferAmount > sourceAccount.Balance)
                {
                    Console.WriteLine("Invalid transfer amount.");
                    continue;
                }
                Console.WriteLine($"\nTransfering {transferAmount} {sourceAccount.Currency} from {sourceAccount.AccountName}.\n");
                Console.WriteLine("Enter the account number you want to transfer to or press Enter to return:");
                if (key.Key == ConsoleKey.Enter)
                {
                    break; // Exit the loop if Enter is pressed to return to the menu
                }
                else if (!int.TryParse(Console.ReadLine(), out transferToOrder))
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                    continue;
                }

                Account targetAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferToOrder);

                if (targetAccount == null)
                {
                    Console.WriteLine("Target account not found."); //if sourceacc == targetacc > felmeddelande
                    continue;
                }
                Converter converter = new Converter();
                decimal money = converter.ConvertMoney(sourceAccount, targetAccount, transferAmount);

                Console.WriteLine($"{transferAmount} {sourceAccount.Currency} will become {money} {targetAccount.Currency}, proceed?\n\n Press Enter to return to account choice.");
                Console.WriteLine("[1]. Yes");
                Console.WriteLine("[2]. No");

                if (int.TryParse(Console.ReadLine(), out int confirm) && confirm == 1)
                {
                    sourceAccount.Balance -= transferAmount;
                    targetAccount.Balance += money;

                    Transaction transaction = new Transaction
                    {
                        TransactionType = "Transfer money",
                        Amount = transferAmount,
                        Timestamp = DateTime.Now
                    };
                    loggedInCustomer.TransactionHistory.Add(transaction);

                    Console.WriteLine($"Transaction successful. \n{transferAmount} {sourceAccount.Currency} was transfered from {sourceAccount.AccountName} to {targetAccount.AccountName}"); 
                }
                else
                {
                    Console.WriteLine("Transaction cancelled.");
                }

                Console.WriteLine("Press Enter to return to account choice or any other key to exit.");
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    break; // Exit the loop if any key other than Enter is pressed
                }
            }

            Console.Clear();
            CustomerManager.Menu(loggedInCustomer);
        }

        /// <summary>
        /// Nathalee:
        /// Metod för att föra över pengar till andra egna konton.
        /// Förbättringsförslag från mig själv inkluderar: hitta ett smidigt sätt att läsa in användaren så att det 
        /// enkelt går att skicka tillbaka hen till menyn. 
        /// Ändra så det inte är accountnumber som används i sökfunktionen för egna konton, se över om det är smidigast att lägga in en parameter i IAccounts
        /// eller att använda metoder i list för detta.
        /// Se över hur vi ska koppla till dictionary för att sökfunktionen ska komma åt andra användare. 
        /// Skapa logik för koppling till andra användare.
        /// Utökade failsafes för att säkerställa att det är rätt mängd pengar som förs över samt att de förs över till rätt person.
        /// Ny metod i annan klass för att lagra informationen som skett i denna klass för att kunna komma åt historik.
        /// Valuta?
        /// </summary>
        /// <param name = "loggedInUser" ></ param >
        public static void TransferMoeneyToOthers(Customer loggedInCustomer)  // rename
        {
            while (true)
            {
                Console.Clear();
                ShowAllAcc(loggedInCustomer);

                Console.WriteLine("Enter the number of the account you want to transfer from:");
                if (!int.TryParse(Console.ReadLine(), out int transferFromOrder))
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                    continue;
                }

                Account sourceAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferFromOrder);

                if (sourceAccount == null)
                {
                    Console.WriteLine("Source account not found.");
                    continue;
                }

                Console.WriteLine("Enter the amount to transfer:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount) || transferAmount <= 0 || transferAmount > sourceAccount.Balance)
                {
                    Console.WriteLine("Invalid transfer amount.");
                    continue;
                }
                Account targetAccount;

                Console.WriteLine("Enter the account number of the person you would like to transfer to:");


                if (!int.TryParse(Console.ReadLine(), out int transferToAccountNr))
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                    continue;
                }

                bool targetAccountFound = FindAndTransfer(loggedInCustomer, transferFromOrder, transferAmount, transferToAccountNr);

                if (!targetAccountFound)
                {
                    Console.WriteLine("Target account not found.");
                    continue;
                }

                Console.WriteLine("Press Enter to return to account choice or any other key to exit.");
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    break; // Exit the loop if any key other than Enter is pressed
                }
            }

            Console.Clear();
            CustomerManager.Menu(loggedInCustomer);


        }
        public static bool FindAndTransfer(Customer loggedInCustomer, int transferFromOrder, decimal transferAmount, int transferToAccountNr)
        {
            foreach (KeyValuePair<string, Customer> user in DataManager.customerList)
            {
                if (user.Value.Accounts.Any(acc => acc.AccountNumber == transferToAccountNr))
                {
                    Customer targetCustomer = user.Value;
                    Account targetAccount = targetCustomer.Accounts.Find(a => a.AccountNumber == transferToAccountNr);

                    if (targetAccount != null)
                    {
                        // Assuming your transfer logic here...
                        Account sourceAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferFromOrder);
                        if (sourceAccount != null && sourceAccount.Balance >= transferAmount)
                        {
                            Converter converter = new Converter();
                            decimal money = converter.ConvertMoney(sourceAccount, targetAccount, transferAmount);
                            sourceAccount.Balance -= transferAmount;
                            targetAccount.Balance += money;

                            Transaction transaction = new Transaction
                            {
                                TransactionType = "Transfer money",
                                Amount = transferAmount,
                                Timestamp = DateTime.Now
                            };
                            loggedInCustomer.TransactionHistory.Add(transaction);

                            Console.WriteLine("Transaction successful.");
                            return true; // Found and completed transfer
                        }
                    }
                }
            }

            return false; // Target account not found

        }




        public static void ShowAllAcc(Customer loggedInCustomer)
        {
            Console.WriteLine("Here are your accounts:\n");

            foreach (Account account in loggedInCustomer.Accounts)
            {
                Console.WriteLine($"{account.SortOrder}.");
                Console.WriteLine($"Account name: {account.AccountName}");
                Console.WriteLine($"Account number: {account.AccountNumber}");
                Console.WriteLine($"Currency: {account.Currency}");
                Console.WriteLine($"Balance: {account.Balance}\n");
            }
        }
       

        //Account targetAccount = null;

        //Console.Clear();
        //Console.WriteLine("Here are your accounts:\n");
        //foreach (Account account in loggedInCustomer.Accounts)
        //{
        //    Console.WriteLine($"{account.SortOrder}.");
        //    Console.WriteLine($"Account name: {account.AccountName}");
        //    Console.WriteLine($"Account number:{account.AccountNumber}");
        //    Console.WriteLine($"Currency:{account.Currency}");
        //    Console.WriteLine($"Balance:{account.Balance}\n");
        //}

        //while (true)
        //{
        //    Console.WriteLine("Which account do you want to transfer from?");
        //    Console.WriteLine("Please press \"enter\" to go to meny.");
        //    string transferFromAcc = Console.ReadLine();

        //    if (string.IsNullOrEmpty(transferFromAcc))
        //    {
        //        Console.Clear();
        //        CustomerManager.Menu(loggedInCustomer);
        //        return;
        //    }

        //    if (!int.TryParse(transferFromAcc, out int transferFromOrder))
        //    {
        //        Console.WriteLine("Invalid input. Please enter a valid account number.");
        //        return;
        //    }

        //    //var backkey = Console.ReadKey(intercept: true); // intercept = true prevents the entered key from being displayed

        //    //if (backkey.Key == ConsoleKey.Enter)
        //    //{
        //    //    Console.Clear();
        //    //    CustomerManager.Menu(loggedInCustomer);
        //    //    return;
        //    //}
        //    // If the entered key is not Enter, proceed to check for integer input
        //    //else if (!int.TryParse(backkey.KeyChar.ToString(), out transferFromAcc))
        //    //{
        //    //    Console.WriteLine("Invalid input. Please enter a number corresponding to the account you wish to transfer from or press 'Enter' to return to the Menu.");
        //    //    continue;
        //    //}
        //    //if (!int.TryParse(transferFromAcc, out int transferFromOrder))
        //    //{
        //    //    Console.WriteLine("Invalid input. Please enter a valid account number.");
        //    //    return;
        //    //}

        //    Account sourceAccount = loggedInCustomer.Accounts.Find(a => a.SortOrder == transferFromOrder);
        //    if (sourceAccount == null)
        //    {
        //        Console.WriteLine("Account not found.");
        //        return;
        //    }



        //    while (transferFromAcc == account.SortOrder)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"You have chosen to transfer from {account.AccountName}.\n");
        //        Console.WriteLine("Which account would you like to transfer from?");
        //        Console.WriteLine("Press Enter return to Account view.");
        //        int transferToAcc;
        //        if (backkey.Key == ConsoleKey.Enter)
        //        {
        //            Console.Clear();
        //            break;
        //        }
        //        else if (!int.TryParse(backkey.KeyChar.ToString(), out transferToAcc))
        //        {
        //            Console.WriteLine("Invalid input. Please enter a number corresponding to the account you wish to transfer from or press 'Enter' to return to Account view.");
        //            continue;
        //        }

        //        targetAccount = loggedInCustomer.Accounts.Find(a => a.AccountNumber == transferToAcc); //Ändra till att söka efter nummer på Acc i listan, ändra i Acc eller utgår från List-metod? /N

        //        sourceAccount = loggedInCustomer.Accounts.Find(a => a.AccountNumber == transferFromAcc); //Ändra till att söka efter nummer på acc i listan
        //        Console.WriteLine("How much money do you want to transfer?");
        //        decimal transferAmount; // Ensure valid input
        //        try
        //        {
        //            transferAmount = Convert.ToDecimal(Console.ReadLine());
        //        }
        //        catch (FormatException)
        //        {
        //            Console.WriteLine("Incorrect input, please enter a numeric value.");
        //            continue;
        //        }
        //        if (transferAmount < 0 || transferAmount > sourceAccount.Balance)
        //        {
        //            Console.WriteLine("Invalid transfer amount.");
        //            return;
        //        }
        //        decimal money = Converter.ConvertMoney(sourceAccount, targetAccount, transferAmount);
        //        Console.WriteLine($"{transferAmount} {sourceAccount.Currency} blir {money} {targetAccount.Currency}, okidoki?");
        //        sourceAccount.Balance -= transferAmount;
        //        targetAccount.Balance += money;   // Ändrade här så att konverterade amount kommer att sättas in
        //        Transaction transaction = new Transaction
        //        {
        //            TransactionType = "Transfer money",
        //            Amount = transferAmount,
        //            Timestamp = DateTime.Now
        //        };
        //        loggedInCustomer.TransactionHistory.Add(transaction);

        //        //Lägg till information till användaren om trasaktionen. /N
        //        //Lägg till metod för att lagra informationen i historik. /N
        //        Console.WriteLine("Kundmeddelande");
        //        Console.ReadKey();
        //        Console.Clear();
        //        CustomerManager.Menu(loggedInCustomer);
    }

}
