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
    public class ClientServerMessanger 
    {
        private WebSocket _websocket;
        private MessageHandler _msghandler;

        public ClientServerMessanger(){ }
    
        public ClientServerMessanger(string url)
        {
            _websocket = new WebSocket(url);
            _msghandler = new MessageHandler();

            configure();
        }

        private void configure() 
        {
            _websocket.OnMessage += (sender, e) =>
            {
                if (e.IsText)
                {
                    JustForTestRepository.AllMessages.Add(_msghandler.DeserializeMessage(e.Data));
                }
            };
        }

        public void Connect()
        {
            _websocket.Connect();
        }

        public void Write(Message message)
        {
            _websocket.Send(_msghandler.SerializeMessage(message));
        }
    }
}
