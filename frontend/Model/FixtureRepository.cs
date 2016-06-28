using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace AllyTalksClient.Model {
    public class FixtureRepository {
        static FixtureRepository()
        {
            AvisCollection = new Dictionary<string, BitmapImage>();
        }

        public FixtureRepository()
        {
            Contacts = LoadContacts();
            Messages = new ObservableCollection<Message.Message>();
            History = LoadHistory();
        }

        public User CurrentUser { get; set; }
        public User CurrentReceiver { get; set; }
        public static Dictionary<string, BitmapImage> AvisCollection { get; set; }

        public ObservableCollection<User> Contacts { get; }

        public ObservableCollection<Message.Message> Messages { get; private set; }

        public Dictionary<string, ObservableCollection<Message.Message>> History { get; }

        private ObservableCollection<User> LoadContacts()
        {
            var collection =
                JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText("contacts.json"));

            collection.ToList().ForEach(x => AvisCollection.Add(x.Login, LoadBitmapImage(x.Login)));

            return collection;
        }

        public void SetMessages(string login)
        {
            Messages = History[login];
            CurrentReceiver = Contacts.First(n => n.Login == login);
        }

        private Dictionary<string, ObservableCollection<Message.Message>> LoadHistory()
        {
            return JsonConvert
                .DeserializeObject<Dictionary<string, ObservableCollection<Message.Message>>>(
                    File.ReadAllText("history.json")
                );
        }

        private BitmapImage LoadBitmapImage(string login)
        {
            var bi = new BitmapImage();

            var baseUrl = ConfigurationManager.ConnectionStrings["ApiServer"].ConnectionString;
            var url = $"{baseUrl}/userpictures/{login}.jpg";

            var request = WebRequest.Create(url) as HttpWebRequest;

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = request.GetResponse().GetResponseStream();
            bi.EndInit();

            return bi;
        }
    }
}