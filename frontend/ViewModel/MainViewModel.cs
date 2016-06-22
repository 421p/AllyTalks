using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace AllyTalksClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ClientServerMessenger _messenger;

        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand ConnectWsCommand { get; set; }

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
            _messenger = new ClientServerMessenger("ws://127.0.0.1:7777");

            SendMessageCommand = new RelayCommand(SendMessage);
            ConnectWsCommand = new RelayCommand(ConnectWs);
           
            CurrentReceiver = JustForTestRepository.AllFriends[0]; //for testing
        }

        private void SendMessage()
        {
            _messenger.Write(CurrentMessage);

            IsNewItemInContainer = true;
            CurrentMessage = null;
        }

        private void ConnectWs()
        {
            _messenger.Connect();
        }
    }
}