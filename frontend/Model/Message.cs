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
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string Token { get; set; }

        public Message() { }

        public Message(string receiver, string type, string token)
        {
            //Sender = JustForTestRepository.CurrentUser.Login;
            Receiver = receiver;
            Type = type;
            //Time = DateTime.Now.ToShortTimeString();
            Token = token;
        }
    }
}
