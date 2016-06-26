using Newtonsoft.Json;
namespace AllyTalksClient.Model {
    public class Message {
        public Message()
        {
        }

        public Message(string receiver, string type, string token)
        {
            //Sender = JustForTestRepository.CurrentUser.Login;
            Receiver = receiver;
            Type = type;
            //Time = DateTime.Now.ToShortTimeString();
            Token = token;
        }

        [JsonProperty(PropertyName = "sender")]
        public string Sender { get; set; }

        [JsonProperty(PropertyName = "receiver")]
        public string Receiver { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}