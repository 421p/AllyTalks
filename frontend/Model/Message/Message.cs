using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace AllyTalksClient.Model.Message {
    public class Message {
        public Message()
        {
        }

        public Message(string type, string token, string receiver = null)
        {
            Receiver = receiver;
            Type = type;
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

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonIgnore]
        public BitmapImage Picture => FixtureRepository.AvisCollection[Sender];
    }
}