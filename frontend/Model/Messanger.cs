using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using AllyTalksClient.Model.Message;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace AllyTalksClient.Model {
    public class Messenger {
        private readonly WebSocket _websocket;
        private readonly FixtureRepository _repo;

        public Messenger(FixtureRepository repo)
        {
            _repo = repo;
        }

        public Messenger(string url, FixtureRepository repo)
        {
            _websocket = new WebSocket(url);
            _repo = repo;
            Configure();
        }

        private void Configure()
        {
            _websocket.OnMessage +=
                (sender, e) => {
                    Message.Message msg = MessageSerializer.DeserializeMessage(e.Data);
                    switch (msg.Type)
                    {
                        case MessageType.Message:
                            if (msg.Sender == _repo.CurrentReceiver.Login)
                                DispatchIt(() => _repo.Messages.Add(msg));
                            else
                                DispatchIt(() => _repo.History[msg.Sender].Add(msg));
                            break;
                        case MessageType.Error:
                            DispatchIt(() => _repo.Messages.Add(msg));
                            break;
                    }
                  
                };
        }

        public void Connect()
        {
            _websocket.Connect();
        }

        private void DispatchIt(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, action);
        }

        public void Write(Message.Message message)
        {
            _websocket.Send(MessageSerializer.SerializeMessage(message));
        }

        public string GetAuthToken(string login, string password)
        {
            string token;

            var url = ConfigurationManager.ConnectionStrings["ApiServer"].ConnectionString;

            try {
                using (var client = new WebClient()) {
                    var response =
                        client.UploadValues($"{url}/api/auth", new NameValueCollection {
                            {"login", login},
                            {"password", password}
                        });

                    var joResponse = JObject.Parse(Encoding.UTF8.GetString(response));
                    token = joResponse["token"].ToString();
                }
            }
            catch (Exception) {
                token = string.Empty;
            }

            return token;
        }

        public bool IsConnected()
        {
            return _websocket.IsAlive;
        }
    }
}