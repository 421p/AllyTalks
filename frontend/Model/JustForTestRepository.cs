using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AllyTalksClient.Model {
    public static class JustForTestRepository {
        public static User CurrentUser { get; set; }

        private static ObservableCollection<User> _allFriends;

        private static ObservableCollection<Message> _allMessages;

        private static Dictionary<string, ObservableCollection<Message>> _dataInDB;

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
                if (_allMessages==null)
                    _allMessages = new ObservableCollection<Message>();
                return _allMessages;
            }
            set {
                _allMessages = value;
            }
        }

        public static Dictionary<string, ObservableCollection<Message>> DataInDB
        {
            get
            {
                if (_dataInDB == null)
                    _dataInDB = GetData();
                return _dataInDB;
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

        public static void SetMessages(string login)
        {
            AllMessages = DataInDB[login];
        }

        private static Dictionary<string, ObservableCollection<Message>> GetData()
        {
            var data = new Dictionary<string, ObservableCollection<Message>>();


            data.Add("klepach", new ObservableCollection<Message>(){new Message() { Text = "Prepod", Receiver = "sher", Type = "message" }});
            data.Add("alina", new ObservableCollection<Message>() { new Message() { Text = "Alya", Receiver = "sher", Type = "message" } });
            data.Add("sher", new ObservableCollection<Message>() { new Message() { Text = "Durak", Receiver = "sher", Type = "message" } });
            return data;
        }
    }
}