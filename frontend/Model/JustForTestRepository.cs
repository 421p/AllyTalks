using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AllyTalksClient.Model {
    public static class JustForTestRepository {
        public static User CurrentUser { get; set; }

        private static ObservableCollection<User> _allFriends;

        private static ObservableCollection<Message> _allMessages;


        public static ObservableCollection<User> AllFriends {
            get {
                if (_allFriends == null)
                    _allFriends = GenerateFriends();
                return _allFriends;
            }
        }

        public static ObservableCollection<Message> AllMessages
        {
            get {
                if (_allMessages == null)
                    _allMessages = new ObservableCollection<Message>();
                return _allMessages;
            }
        }

        private static ObservableCollection<User> GenerateFriends()
        {
            var tmp = new ObservableCollection<User>();
            tmp.Add(new User("klepach", "klepach",
                "http://www.mediaport.ua/sites/default/files/mp/images/Pyrlik/1390902766_klichko-vitaliy.jpeg"));
            tmp.Add(new User("sher", "Shereshovets", "https://pbs.twimg.com/profile_images/582281068503531522/XxZ0QWV_.jpg"));
            tmp.Add(new User("alina", "alina", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBREXRdjdXBs79e9fNb6TzBTbA8jT9s24dGUjrv2Ajs_yEvETr"));
            return tmp;
        }

        private static Dictionary<string, ObservableCollection<Message>> CreateChatRooms()
        {
            var tmp = new Dictionary<string, ObservableCollection<Message>>();

            foreach (var friend in AllFriends)
            {
                tmp.Add(friend.Login, new ObservableCollection<Message>());
            }

            return tmp;
        }
    }
}