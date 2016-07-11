using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using GalaSoft.MvvmLight;

namespace AllyTalksClient.Model {
    public class User : ViewModelBase{
        private string _nickname;

        public User()
        {
        }

        public User(string login, string name = null)
        {
            Login = login;
            Nickname = name;
        }
       
        public string Nickname
        {
            get { return _nickname; }
            set {
                _nickname = value;
                RaisePropertyChanged("Nickname");
            }
        }

        public string Login { get; set; }

        [JsonIgnore]
        public BitmapImage Picture => FixtureRepository.AvisCollection[Login];
    }
}