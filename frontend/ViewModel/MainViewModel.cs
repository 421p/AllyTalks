using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using AllyTalksClient.Model;
using AllyTalksClient.Model.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Messenger = AllyTalksClient.Model.Messenger;

namespace AllyTalksClient.ViewModel {
    public class MainViewModel : ViewModelBase {
        private readonly Messenger _messenger;
        private string _token;
        private string _info;
        private Message _currentMessage;
        private User _currentReceiver;
        private User _currentUser;
        private bool _isNewItemInContainer;
        private ObservableCollection<Message> _messages;
        private ObservableCollection<User> _users;
        private readonly FixtureRepository _repo;

        public MainViewModel()
        {
            _repo = new FixtureRepository();

            _messenger = new Messenger(
                ConfigurationManager.ConnectionStrings["ServerConnection"].ConnectionString,
                _repo
            );

            SendMessageCommand = new RelayCommand(SendMessage);
            StartPageLoadedCommand = new RelayCommand(StartPageLoaded);
            SignInCommand = new RelayCommand<object>(SignIn);
            SignOutCommand = new RelayCommand(SignOut);
            ExitCommand = new RelayCommand(Exit);
            CurrentReceiver = _repo.Contacts[0];
        }

        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand StartPageLoadedCommand { get; set; }
        public RelayCommand<object> SignInCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }


        public User CurrentReceiver {
            get { return _currentReceiver; }
            set {
                if (_currentReceiver != value){
                    _currentReceiver = value;
                    SetChatRoom();
                }
                RaisePropertyChanged("CurrentReceiver");
            }
        }

        public Message CurrentMessage {
            get { return _currentMessage ?? (_currentMessage = new Message {
                Type = MessageType.Message,
                Token = _token
            }) ; }
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

        public ObservableCollection<Message> Messages {
            get {
                return _messages = _repo.Messages;
            }
            set {
                _messages= value;
            }
        }

        public ObservableCollection<User> Users => _users ?? (_users = _repo.Contacts);

        public bool IsNewItemInContainer {
            get { return _isNewItemInContainer; }
            set {
                _isNewItemInContainer = value;
                RaisePropertyChanged("IsNewItemInContainer");
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged("Info");
            }
        }

        private void SendMessage()
        {
            CurrentMessage.Receiver = CurrentReceiver.Login;
            CurrentMessage.Sender = CurrentUser.Login;

            _messenger.Write(CurrentMessage);
            Messages.Add(CurrentMessage);

            IsNewItemInContainer = !IsNewItemInContainer;
            CurrentMessage = null;
        }

        private void StartPageLoaded()
        {
            if (ConfigurationManager.AppSettings["login"] != string.Empty &&
                ConfigurationManager.AppSettings["password"] != string.Empty) {
                AuthUser(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);
                CurrentUser.Login = ConfigurationManager.AppSettings["login"];
            }
        }

        private void SignIn(object parameter)
        {
            try {
                if (AuthUser(CurrentUser.Login, (parameter as PasswordBox).Password)) {
                   SetConfigData(CurrentUser.Login, (parameter as PasswordBox).Password);
                }
            }
            catch (Exception ex) {
                Info = ex.Message;
            }
        }

        private void SignOut()
        {
            SetConfigData();
            RestartApp();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
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

        
        private bool AuthUser(string login, string password)
        {
            _token = _messenger.GetAuthToken(login, password);

            if (_token == string.Empty) {
                throw new Exception("Authorization failed! Please check your login and password, then try again!");
            }

            _messenger.Connect();
            _messenger.Write(new Message(MessageType.Auth, _token, "service"));
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new NotificationMessage("ShowMainWindow"));
            return true;
        }

        private void SetChatRoom()
        {
           _repo.SetMessages(CurrentReceiver.Login);
           GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(Messages);
        }
    }
}