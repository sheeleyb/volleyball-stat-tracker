using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrackerGlobal.Shared
{
    public class TeamCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public TeamCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public TeamCredentials()
        {
            Username = "";
            Password = "";
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password));
        }
    }
}
