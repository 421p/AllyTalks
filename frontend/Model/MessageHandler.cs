using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public static class MessageHandler 
    {
        public static string SerializeMessage(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static Message DeserializeMessage(string data)
        {
            var json = JObject.Parse(data);
            var type = (string)json["Type"];
            Message message = new Message();

            if (type == "message")
            {
                message = JsonConvert.DeserializeObject<Message>(data);
            }

            return message;
        }
    }
}
