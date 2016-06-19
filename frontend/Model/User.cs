using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public class User
    {
        public string Login { get; set; } 
        public string Nickname { get; set; }
        public string Picture { get; set; }

        public User()
        {

        }

        public User(string login, string name, string picture)
        {
            Login = login;
            Nickname = name;
            Picture = picture;
        }
    }

}
