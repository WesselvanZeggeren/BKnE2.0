using BKnE2Client.client.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.controller
{
    public class Controller
    {
        private MessageHandler messageHandler;
        private NetworkConnection networkConnection;

        public Controller()
        {
            this.messageHandler = new MessageHandler(this);
            this.networkConnection = new NetworkConnection(this);
        }

        public void HandleMessage(string message)
        {
            messageHandler.Invoke(message);
        }

        //Connect and login
        public void Login(string username, string password)
        {
            this.networkConnection.Connect(ClientConfig.host, ClientConfig.port);
            //Login
        }

        //Connect and register
        public void Register(string username, string password)
        {
            this.networkConnection.Connect(ClientConfig.host, ClientConfig.port);
            //Register
        }
    }
}
