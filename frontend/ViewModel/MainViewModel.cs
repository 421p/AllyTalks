using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;

namespace AllyTalksClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    
    public class MainViewModel : ViewModelBase
    {
        /*
        ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                if (_clients == null)
                    _clients = ClientRepository.AllClients; //связь с моделью
                return _clients;
            }
        }
        */
        ObservableCollection<UserViewModel> _userViewModels;

        public ObservableCollection<UserViewModel> UserViewModels
        {
            get
            {
                if (_userViewModels == null)
                {
                    UserViewModels = new ObservableCollection<UserViewModel>();
                    foreach (var friend in JustForTestRepository.AllFriends)
                    {
                        UserViewModels.Add(new UserViewModel(friend));
                    }
                }
                return _userViewModels; 
            }
            set
            {
                _userViewModels = value;
                RaisePropertyChanged("UserViewModels");
            }
        }

        ObservableCollection<MessageViewModel> _messageViewModels;

        public ObservableCollection<MessageViewModel> MessageViewModels
        {
            get 
            {
                if (_messageViewModels == null)
                {
                    MessageViewModels = new ObservableCollection<MessageViewModel>();
                    foreach (var message in JustForTestRepository.AllMessages)
                    {
                        MessageViewModels.Add(new MessageViewModel(message));
                    }
                }
                return _messageViewModels; 
            }
            set
            {
                _messageViewModels = value;
                RaisePropertyChanged("MessageViewModels");
            }
        }

        private User _sender;

        public User Sender
        {
            get
            {
                if (_sender == null)
                    _sender = new User();
                return _sender;
            }
            set
            {
                _sender = value;
                RaisePropertyChanged("Sender");
            }
        }

        /// <summary>
        /// Receiver - SelectedItem from Listbox 
        /// will be programmed later
        /// </summary>

        private User _receiver;

        public User Receiver
        {
            get
            {
                if (_receiver == null)
                    _receiver = new User();
                return _receiver;
            }
            set
            {
                _receiver = value;
                RaisePropertyChanged("Receiver");
            }
        }

        private Message _currentMessage;

        public Message CurrentMessage
        {
            get
            {
                if (_currentMessage == null)
                    _currentMessage = 
                        new Message() { Type = "message", Time = DateTime.Now, ReceiverID = Receiver.Id, SenderID = Sender.Id }; //time should be somehow get from server, not sent by client
                return _currentMessage;
            }
            set
            {
                _currentMessage = value;
                RaisePropertyChanged("CurrentMessage");
            }
        }

        private ClientServerMessanger _messanger;

        public ClientServerMessanger Messanger
        {
            get
            {
                if (_messanger == null)
                    _messanger = new ClientServerMessanger();
                return _messanger;
            }
            set
            {
                _messanger = value;
                RaisePropertyChanged("Messanger");
            }
        }

        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand ConnectWSCommand { get; set; }
        
        public MainViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);
            ConnectWSCommand = new RelayCommand(ConnectWS);
            
            Sender = new User();
            Sender = JustForTestRepository.CurrentUser;

            Receiver = new User();
            Receiver = JustForTestRepository.AllFriends[0]; //for testing

            Messanger = new ClientServerMessanger("ws://127.0.0.1:7777");
        }

        private void SendMessage()
        {
            Messanger.Write(CurrentMessage);

            //this shows only my messages sent but not received, can not find way to bind changes in model from outside with viewmodel 
            
            _messageViewModels.Add(new MessageViewModel(CurrentMessage));
            
            CurrentMessage = null;
            MessageViewModels = null;
        }

        private void ConnectWS()
        {
            Messanger.Connect();
        }
    }
}