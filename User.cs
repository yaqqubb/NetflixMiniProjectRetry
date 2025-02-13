using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNetflix
{
    public class User : BaseModel
    {
       

        public User(string username, UserType role, string password)
        {
            Username = username;
            Role = role;
            Password = password;
        }

        public string Username { get; set; }
        public UserType Role { get; set; }
        public string Password { get; set; }
        public List<Movie> Watchlist { get; set; } = new List<Movie>();
    }
}
