using System.Collections.ObjectModel;

namespace AllyTalksClient.Model {
    public static class JustForTestRepository {
        private static User _currentUser;

        private static ObservableCollection<User> _allFriends;

        private static ObservableCollection<Message> _allMessages;

        public static User CurrentUser {
            get {
                _currentUser = DetermineCurrentUser();
                return _currentUser;
            }
        }

        public static ObservableCollection<User> AllFriends {
            get {
                if (_allFriends == null)
                    _allFriends = GenerateFriends();
                return _allFriends;
            }
        }

        public static ObservableCollection<Message> AllMessages {
            get {
                if (_allMessages == null)
                    _allMessages = new ObservableCollection<Message>();
                return _allMessages;
            }
        }


        private static ObservableCollection<User> GenerateFriends()
        {
            var tmp = new ObservableCollection<User>();
            tmp.Add(new User("klei", "Vitalya",
                "http://www.mediaport.ua/sites/default/files/mp/images/Pyrlik/1390902766_klichko-vitaliy.jpeg"));
            tmp.Add(new User("2", "Shereshovets", "https://pbs.twimg.com/profile_images/582281068503531522/XxZ0QWV_.jpg"));
            return tmp;
        }

        private static User DetermineCurrentUser()
        {
            return new User("3", "Kotik", "https://pbs.twimg.com/profile_images/582281068503531522/XxZ0QWV_.jpg");
        }
    }
}