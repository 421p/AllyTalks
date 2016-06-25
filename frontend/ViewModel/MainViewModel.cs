using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AllyTalksClient.ViewModel {
    public class MainViewModel : ViewModelBase {
        private Message _currentMessage;

        //should be realized for sending private messages
        private User _currentReceiver;

        private User _currentUser;

        private bool _isNewItemInContainer;


        private ObservableCollection<Message> _messages;
        private readonly ClientServerMessenger _messenger;
        private string _token;

        private ObservableCollection<User> _users;

        public MainViewModel()
        {
            _messenger =
                new ClientServerMessenger(ConfigurationManager.ConnectionStrings["ServerConnection"].ConnectionString);

            SendMessageCommand = new RelayCommand(SendMessage);
            ConnectWsCommand = new RelayCommand(ConnectWs);
            SignInCommand = new RelayCommand<object>(SignIn);
            SignOutCommand = new RelayCommand(SignOut);

            CurrentReceiver = JustForTestRepository.AllFriends[0]; //for testing
        }

        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand ConnectWsCommand { get; set; }
        public RelayCommand<object> SignInCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        public User CurrentReceiver {
            get { return _currentReceiver ?? (_currentReceiver = new User()); }
            set {
                _currentReceiver = value;
                RaisePropertyChanged("CurrentReceiver");
            }
        }

        public Message CurrentMessage {
            get { return _currentMessage ?? (_currentMessage = new Message(CurrentReceiver.Login, "message", _token)); }
            set {
                _currentMessage = value;
                RaisePropertyChanged("CurrentMessage");
            }
        }

        public User CurrentUser {
            get { return _currentUser ?? (_currentUser = new User()); }
            set {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public ObservableCollection<Message> Messages => _messages ?? (_messages = JustForTestRepository.AllMessages);

        public ObservableCollection<User> Users => _users ?? (_users = JustForTestRepository.AllFriends);

        public bool IsNewItemInContainer {
            get { return _isNewItemInContainer; }
            set {
                _isNewItemInContainer = value;
                RaisePropertyChanged("IsNewItemInContainer");
            }
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
            var login = CurrentUser.Login;
            var password = (parameter as PasswordBox).Password;
            _token = _messenger.GetAuthToken(login, password);

            Console.WriteLine(_token);

            if (_token != string.Empty) {
                SetConfigData(login, password);
                RestartApp();
                _messenger.Write(new Message("service", "auth", _token));
            }
        }

        private void SignOut()
        {
            SetConfigData();
            RestartApp();
        }

        private void SetConfigData(string login = "", string password = "")
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
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