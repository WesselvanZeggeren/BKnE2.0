using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BKnE2Server.server.model.game;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.helpers;
using BKnE2Server.server.model.json;

namespace BKnE2Server.server.model.client
{

    class Client
    {

        // attributes
        public Game game;
        private List<Pin> pins;
        private ClientData data = null;

        private Server server;

        private Thread thread;
        private TcpClient client;
        private NetworkStream stream;

        // constructor
        public Client(Server server, TcpClient client)
        {
            this.server = server;

            this.pins = new List<Pin>();

            this.thread = new Thread(handleClientConnection);
            this.thread.Start(client);
        }

        // connection
        private void handleClientConnection(object obj)
        {

            this.client = obj as TcpClient;
            this.stream = client.GetStream();

            while (true)
            {

                this.server.receiveMessage(this, TCPHelper.readText(this.stream));

                Thread.Sleep(10);
            }
        }

        public void sendMessage(string message)
        {

            TCPHelper.sendText(this.stream, message);
        }

        // account
        public void login(string credentialString)
        {

            string[] credentials = credentialString.Split(':');

            if (credentials.Length == 3)
                this.data = AccountManager.login(credentials[0], credentials[1], Convert.ToBoolean(credentials[2]));

            this.sendMessage(Config.loginPreset + (this.data != null));
        }

        public void save()
        {

            if (this.data != null)
                AccountManager.save();
        }
    }
}
