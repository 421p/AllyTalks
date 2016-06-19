using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace AllyTalksClient.Model
{
    public class ClientServerMessenger 
    {
        private WebSocket _websocket;

        public ClientServerMessenger(){ }
    
        public ClientServerMessenger(string url)
        {
            _websocket = new WebSocket(url);

            Configure();
        }

        private void Configure() 
        {
            _websocket.OnMessage += (sender, e) =>
            {
                if (e.IsText)
                {
                    JustForTestRepository.AllMessages.Add(MessageHandler.DeserializeMessage(e.Data));
                    foreach (var item in JustForTestRepository.AllMessages)
                    {
                        Console.WriteLine(item.Text);
                    }
            
                }
            };
        }

        public void Connect()
        {
            _websocket.Connect();
        }

        public void Write(Message message)
        {
            _websocket.Send(MessageHandler.SerializeMessage(message));
        }
    }
}
