using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public class Message
    {
        public int SenderID { get; set; } 
        public int ReceiverID { get; set; } 
        public string Type { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; } 

        public Message()
        {

        }

        public Message(int senderID, int receiverID, string type, string text, DateTime time)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            Type = type;
            Text = text;
            Time = time;
        }

      
    }
}
