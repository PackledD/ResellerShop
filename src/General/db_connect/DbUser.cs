using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.db_connect
{
    public class DbUser
    {
        private string username;
        public string Username { get => username; }
        private string password;
        public string Password { get => password; }

        public DbUser(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
