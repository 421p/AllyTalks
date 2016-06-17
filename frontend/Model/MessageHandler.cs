using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllyTalksClient.Model
{
    public class MessageHandler //static singlton
    {
        public string SerializeMessage(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public Message DeserializeMessage(string data)
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
