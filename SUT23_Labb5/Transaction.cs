using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class Transaction
    {
        /// <summary>
        /// Ändrat till loggedInCustomer /N
        /// 2023-12-16
        /// </summary>
        /// <param name="loggedInCustomer"></param>
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        //Transaction transaction = new Transaction   // Lägga till den här där du vill ha transaction.
        //{
        //    TransactionType = "Checked money in different curency",
        //    Amount = 0,
        //    Timestamp = DateTime.Now
        //};
        //customer.TransactionHistory.Add(transaction);

        public static void ShowTransactionHistory(Customer loggedInCustomer)
        {
            Console.Clear();
            Console.WriteLine("Show history");
            foreach (Transaction transaction in loggedInCustomer.TransactionHistory)
            {
                Console.WriteLine($"Time: {transaction.Timestamp}   Type: {transaction.TransactionType}   Transaction: {transaction.Amount} "); //Lägga till info om från vilket konto + currency > till vilket konto + currency?
            }
            Console.WriteLine("Press enter to return to the menu.");
            Console.ReadKey();
        }
    }

    //public string TransactionType { get; set; }
    //public decimal Amount { get; set; }
    //public DateTime Timestamp { get; set; }        
    ////Transaction transaction = new Transaction   // Lägga till den här där du vill ha transaction.
    ////{
    ////    TransactionType = "Checked money in different curency",
    ////    Amount = 0,
    ////    Timestamp = DateTime.Now
    ////};
    ////customer.TransactionHistory.Add(transaction);

    //public static void ShowTransactionHistory(Customer loggedInCustomer)
    //{
    //    Console.Clear();
    //    if (loggedInCustomer is Customer customer)
    //    {
    //        Console.WriteLine("Show history");
    //        foreach (Transaction transaction in customer.TransactionHistory)
    //        {
    //            Console.WriteLine($"Time: {transaction.Timestamp}   Type: {transaction.TransactionType}   Transaction: {transaction.Amount} ");
    //        }
    //    }
    //    Console.WriteLine("Press enter to return to the menu.");
    //    Console.ReadKey();
    //}
}

