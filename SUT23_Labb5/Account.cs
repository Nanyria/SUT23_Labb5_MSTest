using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public int SortOrder { get; set; }
        public decimal LoanAmount { get; set; }
        public Account(int accountnumber, string accountname, string currency, decimal balance, int sortOrder)

        {
            AccountNumber = accountnumber;
            AccountName = accountname;
            Currency = currency;
            Balance = balance;
            SortOrder = sortOrder;
        }
        public Account() : this(1, "", "No currency selected", 0, 0)
        {

        }

    }
}
