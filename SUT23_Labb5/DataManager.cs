using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    /// <summary>
    /// Ändrat från en User-dictionary till två separata dictionarys för att lagra informationen separat. /N
    /// 2023-12-15
    /// </summary>
    internal class DataManager
    {
        public static Dictionary<string, Customer> customerList = new Dictionary<string, Customer>();  // Är det bättre med ID?
        public static Dictionary<string, Admin> adminList = new Dictionary<string, Admin>();
        static DataManager()
        {
            Initialize();
        }
        /// <summary>
        /// Ändrat klasserna till Customer respektive Admin istället för User. /N
        /// 2023-12-15
        /// </summary>
        public static void Initialize()
        {
            Customer cus1 = new Customer
            {
                UserName = "User1",
                IDNumber = 5001,
                FirstName = "Johan",
                LastName = "Johansson",
                PassWord = "Passwords1!",
                Birthday = "1978-01-01",
                PasswordAttempts = 0,
                Accounts = new List<Account>
                {
                        new Account(50028977, "MainAccount", "SEK", 1234M, 1),
                        new Account(50011265, "SavingAccount", "EUR", 1234M, 2),
                },
                TransactionHistory = new List<Transaction> { },
            };
            Customer cus2 = new Customer()
            {
                UserName = "User2",
                IDNumber = 5002,
                FirstName = "Anna",
                LastName = "Andersson",
                PassWord = "Password2!",
                Birthday = "1988-01-01",
                Accounts = new List<Account>
                {
                    new Account(12344556, "MainAccount", "SEK", 2345M, 1),
                    new Account(23455678, "SavingAccount", "EUR", 2345M, 2),
                },
                TransactionHistory = new List<Transaction> { },
            };
            Customer cus3 = new Customer()
            {
                UserName = "User3",
                IDNumber = 5003,
                FirstName = "Alice",
                LastName = "Karlsson",
                PassWord = "Password3!",
                Email = "Akuce@Karlsson.se",
                Birthday = "1998-01-01",
                Accounts = new List<Account>
                {
                },
                TransactionHistory = new List<Transaction> { },
            };
            Admin ad1 = new Admin()
            {
                UserName = "Admin1",
                IDNumber = 1001,
                FirstName = "Karl",
                LastName = "Karssib",
                PassWord = "Admin!1",
            };
            customerList.Add("User1", cus1);
            adminList.Add("Admin1", ad1);
            customerList.Add("User2", cus2);
            customerList.Add("User3", cus3);
        }
    }
}
