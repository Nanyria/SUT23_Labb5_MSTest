using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    public class Converter
    {
        public  decimal Yen { get; set; } = 14.0M; // ca 14
        public  decimal Euro { get; set; } = 0.089M;  // ca 0.9
        public  void InsertRate() // Here administrater can change the exchange rate.
        {
            bool go = true;
            while (go)
            {
                try
                {
                    Console.WriteLine("Update the exchange rate.");
                    Console.WriteLine("1. Euro\n2. Yen\n3. Back to Menu.");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.Write("Set exchange rate for ");
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Euro: ");
                            Euro = Validator.GetValidDecimal();
                            Console.WriteLine($"1 SEK = {Euro} EUR");
                            break;
                        case 2:
                            Console.Write("Yen: ");
                            Yen = Validator.GetValidDecimal();
                            Console.WriteLine($"1 SEK = {Yen} Yen");
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
        public  decimal FromSekToYen(decimal money) // Administrator must add the rate first.
        {
            return money * Yen;
        }
        public  decimal FromYenToSek(decimal money)
        {
            return money / Yen;
        }
        public  decimal FromSekToEur(decimal money)
        {
            return money * Euro;
        }
        public  decimal FromEurToSek(decimal money)
        {
            return money / Euro;
        }
        public  decimal ConvertMoney(Account selectedAccount, Account targetAccount, decimal amount)  // Customer will do this action. So the exchange rate should be set
        {
            Console.Clear();
            string currency1 = selectedAccount.Currency;
            string currency2 = targetAccount.Currency;
            decimal money;
            switch (currency1)
            {
                case "SEK":
                    if (currency2 == "SEK")
                        return amount;
                    else if (currency2 == "EUR")
                        return FromSekToEur(amount);
                    else if (currency2 == "YEN")
                        return FromSekToYen(amount);
                    break;
                case "EUR":
                    if (currency2 == "EUR")
                        return amount;
                    else if (currency2 == "SEK")
                        return FromEurToSek(amount);
                    else if (currency2 == "YEN")
                    {
                        money = FromEurToSek(amount);
                        money = Math.Round(FromSekToYen(money), 2);
                        return money;
                    }
                    break;
                case "YEN":
                    if (currency2 == "YEN")
                        return amount;
                    else if (currency2 == "SEK")
                        return Math.Round(FromYenToSek(amount), 2);
                    else if (currency2 == "EUR")
                    {
                        money = FromYenToSek(amount);
                        money = Math.Round(FromSekToEur(money), 2);
                        return money;
                    }
                    break;
                default:
                    Console.WriteLine("Sorry, we do not have that choice. You will be directed to the menu.");
                    break;
            }
            return 0;
            Console.ReadKey(); //Den här koden kommer man inte åt /N
        }
        public  string GetValidChoice()
        {
            bool go = true;
            while (go)
            {
                Console.WriteLine("Choose currency to deposit:");
                Console.WriteLine("1. SEK\n2. EUR\n3. YEN\n4. Cancel.");
                int choice = Validator.GetValidInt();
                switch (choice)
                {
                    case 1:
                        return "SEK";
                    case 2:
                        return "EUR";
                    case 3:
                        return "YEN";
                    case 4:
                        return null;
                    defualt:
                        Console.WriteLine("Please enter a number between 1-4.");
                }
            }
            return null;
        }
        public  decimal ConvertDepositMoney(string currrency2, Account selectedAccount, decimal amount)
        {
            decimal money;
            string currency1 = selectedAccount.Currency;
            switch (currrency2)
            {
                case "SEK":  // SEK to another currency
                    if (currency1 == "SEK")
                        return amount;
                    else if (currency1 == "EUR")
                        return Math.Round(FromSekToEur(amount), 2);
                    else if (currency1 == "YEN")
                        return FromSekToYen(amount);
                    break;
                case "EUR": // Euro to another currecny
                    if (currency1 == "SEK")
                        return Math.Round(FromEurToSek(amount), 2);
                    else if (currency1 == "EUR")
                        return amount;
                    else if (currency1 == "YEN")
                    {
                        money = FromEurToSek(amount);
                        money = Math.Round(FromSekToYen(money), 2);
                        return money;
                    }
                    break;
                case "YEN": // Yen to another currency
                    if (currency1 == "SEK")
                        return Math.Round(FromYenToSek(amount), 2);
                    else if (currency1 == "EUR")
                        return amount;
                    else if (currency1 == "YEN")
                    {
                        money = FromYenToSek(amount);
                        money = Math.Round(FromSekToEur(money), 2);
                        return money;
                    }
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You...");
                    break;
            }
            return 0;
        }
        //public static decimal ConvertDepositMoney(Account selectedAccount, decimal amount)  // Customer will do this action. So the exchange rate should be set
        //{
        //    Console.Clear();
        //    string currency1 = selectedAccount.Currency;
        //    string currency2 = Convert.ToString(amount);
        //    decimal money;
        //    switch (currency1)
        //    {
        //        case "SEK":
        //            if (currency2 == "SEK")
        //                return amount;
        //            else if (currency2 == "EUR")
        //                return FromSekToEur(amount);
        //            else if (currency2 == "YEN")
        //                return FromSekToYen(amount);
        //            break;
        //        case "EUR":
        //            if (currency2 == "EUR")
        //                return amount;
        //            else if (currency2 == "SEK")
        //                return FromEurToSek(amount);
        //            else if (currency2 == "YEN")
        //            {
        //                money = FromEurToSek(amount);
        //                money = Math.Round(FromSekToYen(money), 2);
        //                return money;
        //            }
        //            break;
        //        case "YEN":
        //            if (currency2 == "YEN")
        //                return amount;
        //            else if (currency2 == "SEK")
        //                return Math.Round(FromYenToSek(amount), 2);
        //            else if (currency2 == "EUR")
        //            {
        //                money = FromYenToSek(amount);
        //                money = Math.Round(FromSekToEur(money), 2);
        //                return money;
        //            }
        //            break;
        //        default:
        //            Console.WriteLine("Sorry, we do not have that choice. You will be directed to the menu.");
        //            return 0;
        //            break;
        //    }
        //    return 0;
        //    Console.ReadKey();
        //}

        //public static void ConvertMoney(User user)  // Customer will do this action. So the exchange rate should be set
        //{
        //    if(user is Customer customer) 
        //    { 
        //        Account selectedAccount= ShowSpecificAccount(customer);
        //    string currency = selectedAccount.Currency;
        //    switch (GetCurrencyChoice())
        //    {
        //        case 1: // SEK
        //            Console.WriteLine("1. SEK");
        //            if (currency == "SEK")
        //                Console.WriteLine("You have balance in SEK already.");
        //            else if (currency == "EUR")
        //                Console.WriteLine("It would be " + FromEurToSek(selectedAccount.Balance) + " in SEK.");
        //            else if (currency == "YEN")
        //                Console.WriteLine("It would be " + Math.Round(FromYenToSek(selectedAccount.Balance), 2) + " in SEK");
        //            break;
        //        case 2: // EUR
        //            Console.WriteLine("2. EUR");
        //            if (currency == "EUR")
        //                Console.WriteLine("You have balance in EUR already.");
        //            else if (currency == "SEK")
        //                Console.WriteLine("It would be " + FromSekToEur(selectedAccount.Balance) + " in Euro");
        //            else if (currency == "YEN")
        //            {
        //                decimal money = FromYenToSek(selectedAccount.Balance);
        //                Console.WriteLine(money + "SEK");
        //                Console.WriteLine("It would be " + Math.Round(FromSekToEur(money), 2) + "in Euro");
        //            }
        //            break;
        //        case 3: // YEN
        //            Console.WriteLine("3. YEN");
        //            if (currency == "YEN")
        //                Console.WriteLine("You have balance in YEN already.");
        //            else if (currency == "SEK")
        //                Console.WriteLine("It would be " + FromSekToYen(selectedAccount.Balance) + " in yen");
        //            else if (currency == "EUR")
        //            {
        //                decimal money = FromEurToSek(selectedAccount.Balance);
        //                Console.WriteLine("It would be " + Math.Round(FromSekToYen(money), 2) + " in yen");
        //            }
        //            Console.WriteLine();
        //            break;
        //        default:
        //            Console.WriteLine("Sorry, we do not have that choice. You will be directed to the menu.");
        //            return;
        //            break;
        //    }
        //    }

        //}
        private static Account ShowSpecificAccount(Customer loggedInCustomer)
        {
            Console.WriteLine("Choose an account to view the balance:");
            DisplayUserAccounts(loggedInCustomer);
            int selectedAccountIndex = Validator.GetValidInt("Enter the account number: ", 1, loggedInCustomer.Accounts.Count) - 1;
            Account selectedAccount = loggedInCustomer.Accounts[selectedAccountIndex];
            Console.Clear();
            Console.WriteLine($"Balance for Account {selectedAccount.AccountNumber} ({selectedAccount.AccountName}): {selectedAccount.Balance}({selectedAccount.Currency})");
            return selectedAccount;
        }
        private static void DisplayUserAccounts(Customer loggedInCustomer)
        {
            for (int i = 0; i < loggedInCustomer.Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Account {loggedInCustomer.Accounts[i].AccountNumber}: {loggedInCustomer.Accounts[i].AccountName} - {loggedInCustomer.Accounts[i].Currency}");
            }
        }
        private static int GetCurrencyChoice()
        {
            Console.WriteLine("Choose currency: ");
            string currency1 = "1. SEK";
            string currency2 = "2. EUR";
            string currency3 = "3. YEN";
            Console.WriteLine(currency1 + "\n" + currency2 + "\n" + currency3);
            int currencyChoice = Validator.GetValidInt();
            return currencyChoice;
        }

    }

}
