using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AllyTalksClient.Model {
    public static class MessageHandler {
        public static string SerializeMessage(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static Message DeserializeMessage(string data)
        {
            var json = JObject.Parse(data);
            var type = (string) json["Type"];
            var message = new Message();

            if (type == "message") {
                message = JsonConvert.DeserializeObject<Message>(data);
            }

            return message;
        }
    }
}