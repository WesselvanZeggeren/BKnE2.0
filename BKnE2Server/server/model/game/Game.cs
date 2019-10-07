using BKnE2Base;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
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
        private bool running = false;

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

            while (this.running)
            {


            }
        }

        public void turn()
        {

        }

        public bool isRunning()
        {

            return this.running;
        }

        // pin
        public void receivePin(string receivedString)
        {

            string[] pinString = receivedString.Split(':');

            Pin pin = new Pin(Convert.ToInt32(pinString[0]), Convert.ToInt32(pinString[1]));

            Console.WriteLine(pin);
        }

        // client
        public void addClient(Client client)
        {

            this.clients.Add(client);

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
