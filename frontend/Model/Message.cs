using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public class Message
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }

        public Message() { }

        public Message(User receiver, string type)
        {
            Sender = JustForTestRepository.CurrentUser;
            Receiver = receiver;
            Type = type;
            Time = DateTime.Now.ToShortTimeString();
        }
    }
}
