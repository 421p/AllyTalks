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
        public DateTime Time { get; set; } 

        public Message()
        {

        }

        public Message(string sender, string receiver, string type, string text, DateTime time)
        {
            Sender = sender;
            Receiver = receiver;
            Type = type;
            Text = text;
            Time = time;
        }

      
    }
}
