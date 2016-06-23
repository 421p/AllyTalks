using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
                DispatchIt(() => JustForTestRepository.AllMessages.Add(MessageHandler.DeserializeMessage(e.Data)));
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

        public void Write(Message message)
        {
            _websocket.Send(MessageHandler.SerializeMessage(message));
        }
    }
}
