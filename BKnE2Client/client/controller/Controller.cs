using BKnE2Client.client.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.controller
{
    class Controller
    {
        private MessageHandler messageHandler;
        private NetworkConnection networkConnection;

        public Controller()
        {
            this.messageHandler = new MessageHandler(this);
            this.networkConnection = new NetworkConnection(this);
            this.networkConnection.Connect(ClientConfig.host, ClientConfig.port);
        }

        public void HandleMessage(string message)
        {
            messageHandler.Invoke(message);
        }
    }
}
