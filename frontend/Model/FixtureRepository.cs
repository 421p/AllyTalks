using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace AllyTalksClient.Model {
    public class FixtureRepository {
        public User CurrentUser { get; set; }
        public User CurrentReceiver { get; set; }

        public FixtureRepository()
        {
            Contacts = LoadContacts();
            Messages = new ObservableCollection<Message.Message>();
            History = LoadHistory();
        }

        public ObservableCollection<User> Contacts { get; private set; }

        public ObservableCollection<Message.Message> Messages { get; private set; }

        public Dictionary<string, ObservableCollection<Message.Message>> History { get; private set; }

        private ObservableCollection<User> LoadContacts()
        {
            return JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText("contacts.json"));
        }

        public void SetMessages(string login)
        {
            Messages = History[login];
            CurrentReceiver = Contacts.Where(n => n.Login == login).First();
        }

        private Dictionary<string, ObservableCollection<Message.Message>> LoadHistory()
        {
            return JsonConvert
                .DeserializeObject<Dictionary<string, ObservableCollection<Message.Message>>>(
                File.ReadAllText("history.json")
            );
        }
     
    }
}