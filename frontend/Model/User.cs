using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace AllyTalksClient.Model {
    public class User {
        public User()
        {
        }

        public User(string login, string name = null)
        {
            Login = login;
            Nickname = name;
        }

        public string Login { get; set; }
        public string Nickname { get; set; }

        [JsonIgnore]
        public BitmapImage Picture => FixtureRepository.AvisCollection[Login];
    }
}