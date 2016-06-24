using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AllyTalksClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ClientServerMessenger _messenger;
        
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand ConnectWsCommand { get; set; }
        public RelayCommand<object> SignInCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        //should be realized for sending private messages
        private User _currentReceiver;
        public User CurrentReceiver
        {
            get
            {
                if (_currentReceiver == null)
                    _currentReceiver = new User();
                return _currentReceiver;
            }
            set
            {
                _currentReceiver = value;
                RaisePropertyChanged("CurrentReceiver");
            }
        }

        private Message _currentMessage;
        public Message CurrentMessage
        {
            get
            {
                if (_currentMessage == null)
                    _currentMessage = new Message(CurrentReceiver, "message"); 
                return _currentMessage;
            }
            set
            {
                _currentMessage = value;
                RaisePropertyChanged("CurrentMessage");
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = new User();
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }


        ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get
            {
                if (_messages == null)
                {
                    _messages = JustForTestRepository.AllMessages;
                }
                return _messages;
            }
        }

        ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = JustForTestRepository.AllFriends;
                }
                return _users;
            }
        }

         private bool _isNewItemInContainer;
         public bool IsNewItemInContainer
         {
            get
            {
                return _isNewItemInContainer;
            }
            set
            {
                _isNewItemInContainer = value;
                RaisePropertyChanged("IsNewItemInContainer");
            }
        }
        
        public MainViewModel()
        {
            _messenger = new ClientServerMessenger(ConfigurationManager.ConnectionStrings["ServerConnection"].ConnectionString);

            SendMessageCommand = new RelayCommand(SendMessage);
            ConnectWsCommand = new RelayCommand(ConnectWs);
            SignInCommand = new RelayCommand<object>(SignIn);
            SignOutCommand = new RelayCommand(SignOut);
           
            CurrentReceiver = JustForTestRepository.AllFriends[0]; //for testing
        }

        private void SendMessage()
        {
            _messenger.Write(CurrentMessage);

            IsNewItemInContainer = !IsNewItemInContainer;
            CurrentMessage = null;
        }

        private void ConnectWs()
        {
            _messenger.Connect();
        }

        private void SignIn(object parameter)
        {
            SetConfigData(CurrentUser.Login, (parameter as PasswordBox).Password);
            RestartApp();
        }

        private void SignOut()
        {
            SetConfigData();
            RestartApp();
        }

        private void SetConfigData(string login="", string password="")
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["login"].Value = login;
            config.AppSettings.Settings["password"].Value = password;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void RestartApp()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}