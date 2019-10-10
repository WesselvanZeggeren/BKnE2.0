using BKnE2Base;
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

            this.sendAll(Config.startPreset);
        }

        public bool turn()
        {

            return false;
        }

        public bool isRunning()
        {

            return this.running;
        }

        // pin
        public void receivePin(Client client, string jsonPin)
        {

            int[] pinCoördinates = JsonConvert.DeserializeObject<int[]>(jsonPin);

            Pin pin = new Pin(pinCoördinates[0], pinCoördinates[1]);


        }

        // client
        public void addClient(Client client)
        {

            this.clients.Add(client);
            this.sendAll(Config.accountPreset);

            if (this.clients.Count() >= Config.maxPlayersInGame)
                this.startGame();
        }

        public void sendAll(string message)
        {

            foreach (Client client in this.clients)
            {

                client.sendMessage(message);
            }
        }
    }
}
