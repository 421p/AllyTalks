using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public User()
        {

        }

        public User(int id, string name, string picture)
        {
            Id = id;
            Name = name;
            Picture = picture;
        }
    }

}
