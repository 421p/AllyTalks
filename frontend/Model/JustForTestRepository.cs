using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public static class JustForTestRepository
    {
        private static User _currentUser;

        public static User CurrentUser
        {
            get 
            {
                _currentUser = DetermineCurrentUser();
                return _currentUser; 
            }
        }

        private static ObservableCollection<User> _allFriends;
        
        public static ObservableCollection<User> AllFriends
        {
            get
            {
                if (_allFriends == null)
                    _allFriends = GenerateFriends();
                return _allFriends;
            }
        }

        private static ObservableCollection<Message> _allMessages;

        public static ObservableCollection<Message> AllMessages
        {
            get
            {
                if (_allMessages == null)
                    _allMessages = new ObservableCollection<Message>();
                return _allMessages;
            }
        }
        

        private static ObservableCollection<User> GenerateFriends()
        {
            ObservableCollection<User> tmp = new ObservableCollection<User>();
            tmp.Add(new User("1", "Vitalya", "http://www.mediaport.ua/sites/default/files/mp/images/Pyrlik/1390902766_klichko-vitaliy.jpeg"));
            tmp.Add(new User("2", "Shereshovets", "https://pbs.twimg.com/profile_images/582281068503531522/XxZ0QWV_.jpg"));
            return tmp;
        }

        private static User DetermineCurrentUser()
        {
            return new User("3", "Kotik", "http://cdn.grumpycats.com/wp-content/uploads/2016/02/12654647_974282002607537_7798179861389974677_n-758x758.jpg");
        }

        
    }
}
