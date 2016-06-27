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
            var json = JObject.Parse(data);
            var type = (string) json["type"];

            switch (type) {
                case MessageType.Message:
                   return JsonConvert.DeserializeObject<Message>(data);
                case MessageType.Error:
                    return JsonConvert.DeserializeObject<Message>(data);
                case MessageType.Auth:
                    return  JsonConvert.DeserializeObject<Message>(data);
                default:
                    throw new Exception("Can not handle this kind of message yet.");
            }
        }
    }
}