﻿using BKnE2._0.server.controller;
using BKnE2._0.server.model.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2._0.server.model.game
{

    class Game
    {

        private Thread thread;
        private Server server;
        private List<Client> clients;
        private List<Pin> pins;

        public Game(Server server)
        {

            this.server = server;
        }

        private void startGame()
        {

        }

        public void addClient(Client client)
        {

            this.clients.Add(client);
        }

        public bool isRunning()
        {

            return false;
        }
    }
}
