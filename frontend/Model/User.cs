using System.Configuration;
using Newtonsoft.Json;

namespace AllyTalksClient.Model {
    public class User {
        public User()
        {
        }

        public User(string login, string name, string picture)
        {
            Login = login;
            Nickname = name;
        }

        public string Login { get; set; }
        public string Nickname { get; set; }

        [JsonIgnore]
        public string Picture {
            get {
                var url = ConfigurationManager.ConnectionStrings["ApiServer"].ConnectionString;
                return $"{url}/userpictures/{Login}.jpg";
            }
        }
    }
}