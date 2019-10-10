using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.game
{

    class Game
    {

        // attributes
        private List<Pin> pins = new List<Pin>();
        private List<Client> clients = new List<Client>();
        private List<Client> nextRoundClients = new List<Client>();
        private bool running = false;
        private int iterator = 0;

        private Server server;

        // constructor
        public Game(Server server)
        {

            this.server = server;   
        }

        // game
        public void startGame()
        {

            this.running = true;

            this.writeRequestToAll(Request.newRequest(Config.startType));
        }

        public bool isRunning()
        {

            return this.running;
        }

        // pin
        public void receivePin(Client client, Request request)
        {

            Pin pin = new Pin(request.get("x"), request.get("y"));
        }

        // client
        public void addClient(Client client)
        {

            this.clients.Add(client);

            if (this.clients.Count() >= Config.maxPlayersInGame)
                this.startGame();

            Request request = Request.newRequest(Config.accountType);
            //request.add("clients", this.getRequestClients());

            this.writeRequestToAll(request);
        }

        public void writeRequestToAll(Request request)
        {

            foreach (Client client in this.clients)
            {

                client.writeRequest(request);
            }
        }
    }
}
