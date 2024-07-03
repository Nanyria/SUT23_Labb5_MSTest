using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class InterestManager
    {
        private static decimal savingsInterestRate = 0.02M; 
        private static decimal loanInterestRate = 0.05M;

        internal static void SetSavingsInterestRate(decimal newRate)
        {
            savingsInterestRate = newRate;
            Console.WriteLine($"Savings interest rate set to: {savingsInterestRate:P}");
        }

        public static void SetLoanInterestRate(decimal newRate)
        {
            loanInterestRate = newRate;
            Console.WriteLine($"Loan interest rate set to: {loanInterestRate:P}");
        }

        public static void DisplayInterestRates()
        {
            Console.WriteLine($"Current Savings Interest Rate: {savingsInterestRate:P}");
            Console.WriteLine($"Current Loan Interest Rate: {loanInterestRate:P}");
        }

        public static void AdminSetInterestRates()
        {
            bool go = true;
            while (go)
            {
                try
                {
                    Console.WriteLine("Admin, choose an option:");
                    Console.WriteLine("1. Set Savings Interest Rate");
                    Console.WriteLine("2. Set Loan Interest Rate");
                    Console.WriteLine("3. Back to Menu.");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter new Savings Interest Rate: ");
                            decimal newSavingsRate = Validator.GetValidDecimal();
                            InterestManager.SetSavingsInterestRate(newSavingsRate);
                            break;
                        case 2:
                            Console.Write("Enter new Loan Interest Rate: ");
                            decimal newLoanRate = Validator.GetValidDecimal();
                            InterestManager.SetLoanInterestRate(newLoanRate);
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


        internal static decimal SavingsInterestRate()
        {
            return savingsInterestRate;
        }

        internal static decimal GetLoanInterestRate()
        {
            return loanInterestRate;
        }
    }
}
