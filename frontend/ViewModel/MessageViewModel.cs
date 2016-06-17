using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        Message _messageInfo;

        public Message MessageInfo
        {
            get { return _messageInfo; }
            set
            {
                _messageInfo = value;
                RaisePropertyChanged("MessageInfo");
            }
        }

        public MessageViewModel(){ }

        public MessageViewModel(Message messageInfo)
        {
            _messageInfo = new Message();
            _messageInfo = messageInfo;
        }
    }
}
