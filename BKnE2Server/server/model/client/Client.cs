﻿using System;
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

namespace BKnE2Server.server.model.client
{

    class Client
    {

        // attributes
        public Game game;
        private List<Pin> pins;
        private ClientData data;

        private Server server;

        private Thread thread;
        private TcpClient client;
        private NetworkStream stream;

        // constructor
        public Client(Server server, TcpClient client)
        {

            this.server = server;

            this.thread = new Thread(handleClientConnection);
            this.thread.Start(client);
        }

        // connection
        private void handleClientConnection(object obj)
        {

            this.client = obj as TcpClient;
            this.stream = client.GetStream();

            while (true)
                this.server.receiveMessage(this, TCPHelper.readText(this.stream));
        }

        public void sendMessage(string message)
        {

            TCPHelper.sendText(this.stream, message);
        }

        // account
        public void login(string login)
        {

            this.data = JSONHelper.login();
        }

        public void save()
        {

            JSONHelper.save(this.data);
        }
    }
}
