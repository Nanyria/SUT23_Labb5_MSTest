using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class Admin
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IDNumber { get; set; }
        //public string UserRole { get; set; }
        public Admin() : this("No username provided.", 0000, "No firstname provided.", "No lastname provided.", "No password provided")
        {
        }

        public Admin(string username, int IDnumber, string firstname, string lastname, string password)
        {
            UserName = username;
            PassWord = password;
            FirstName = firstname;
            LastName = lastname;
            IDNumber = IDnumber;
        }

    }
}
