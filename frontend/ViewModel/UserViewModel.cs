using AllyTalksClient.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        User _friendInfo;

        public User FriendInfo
        {
            get { return _friendInfo; }
            set
            {
                _friendInfo = value;
                RaisePropertyChanged("FriendInfo");
            }
        }

        public UserViewModel(){ }

        public UserViewModel(User friendInfo)
        {
            _friendInfo = new User();
            _friendInfo = friendInfo;
        }
    }
}
