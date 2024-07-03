using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class RegisterUser
    {
        /// <summary>
        /// Metod för att skicka vidare Admin till olika metoder för att reg Admin eller Customer (grundkod hämtad från originalet RegisterCustomer) 
        /// Förbättringsförslag: Lägg till möjlighet att återgå till menyn via att trycka på enter. /N
        /// </summary>
        /// <param name="loggedInAdmin"></param>
        public static void Register(Admin loggedInAdmin)
        {
            bool go = true;
            while (go)
            {
                try
                {
                    Console.Write("Choose user role:\n");
                    Console.Write("1. Admin\n");
                    Console.Write("2. Customer\n");
                    Console.Write("3. Back to Menu.");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            RegisterNewAdmin.RegisterAdmin(loggedInAdmin);
                            break;
                        case 2:
                            Console.Clear();
                            RegisterNewCustomer.RegisterCustomer(loggedInAdmin);
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

    }
}
