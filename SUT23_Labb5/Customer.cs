using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class Customer : ICustomer
    {

        public string UserName { get; set; }
        public string PassWord { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IDNumber { get; set; }
        //public string UserRole { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Transaction> TransactionHistory { get; set; }
        public List<Customer> LockedOutCustomer { get; set; }

        public int PasswordAttempts { get; set; }
        public Customer() : this("No username provided.", 0000, "No firstname provided.", "No lastname provided.", "No password provided", "no email provided", "0000-00-00", new List<Account>(), new List<Transaction>(), 0)
        {
        }

        public Customer(string username, int IDnumber, string firstname, string lastname, string password, string email, string birthday, List<Account> accounts, List<Transaction> transactions, int passwordAttempts)
        {
            UserName = username;
            PassWord = password;
            FirstName = firstname;
            LastName = lastname;
            IDNumber = IDnumber;
            Email = email;
            Birthday = birthday;
            Accounts = accounts;
            TransactionHistory = transactions;
            PasswordAttempts = passwordAttempts;
        }

        public static void SortingOrder()
        {
            

        }
    }
}
