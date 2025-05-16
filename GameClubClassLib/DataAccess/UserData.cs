using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClubClassLib.DataAccess
{
    public static class UserData
    {
        private static List<string> adminEmails = new List<string>() { "Koen", "Wim", "Sander" };

        public static bool IsValidLogin(string name, string password)
        {
            return adminEmails.Contains(name) && password.Equals("PXL123");
        }
    }
}
