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

            int size = this.getBoardSize(Config.minBoardSize);

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    this.pins.Add(new Pin(x, y));

            Request request = Request.newRequest(Config.startType);
            request.add("size", size);

            this.writeRequestToAll(request);
        }

        public bool isRunning()
        {

            return this.running;
        }

        // pin
        public void receivePin(Client client, Request request)
        {

            Pin pin = this.getPin(request.get("x"), request.get("y"));

            if (this.isCurrentPlayer(client) && pin.isAssigned)
            {

                pin.isAssigned = true;
                client.assignPin(pin);
            }
        }

        private Pin getPin(int x, int y)
        {

            foreach (Pin pin in this.pins)
                if (pin.x == x && pin.y == y)
                    return pin;

            return null;
        }

        // board
        private int getBoardSize(int size)
        {

            if ((size * size) > (this.clients.Count() * Config.maxPinsPerPlayer))
                return size;

            return this.getBoardSize(size + 1);
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

        private bool isCurrentPlayer(Client client)
        {

            return (client.data.id == this.clients.ElementAt(this.iterator).data.id);
        }

        /*
        private List<string[]> getRequestClients()
        {

            List<string[]> clients = new List<string[]>();

            foreach (Client client in this.clients)
                clients.Add(new string[4], client.data.name, )
        }
        */
    }
}
