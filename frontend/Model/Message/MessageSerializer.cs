using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AllyTalksClient.Model.Message {
    public static class MessageSerializer {
        public static string SerializeMessage(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static Message DeserializeMessage(string data)
        {
            return JsonConvert.DeserializeObject<Message>(data);
        }
    }
}