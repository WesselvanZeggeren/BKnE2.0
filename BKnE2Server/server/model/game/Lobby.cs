using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.game
{
    class Lobby
    {

        // attributes
        private List<Client> clients = new List<Client>();

        public Game game;
        private Server server;

        // constructor
        public Lobby(Server server)
        {

            this.server = server;
        }

        // game
        public void startGame()
        {

            this.game = new Game(this.clients);
            this.game.init();

            Request request = Request.newRequest(Config.startType);
            request.add("size", this.game.size);

            this.writeRequestToAll(request);
        }

        public void receiveStart(Client client, Request request)
        {

            if (request.get("start"))
                this.startGame();
            else
                this.removeClient(client);
        }

        // pin
        public void receivePin(Client client, Request request)
        {

            if (this.game.receivePin(client, request))
            {

                request.add("r", client.data.color.r);
                request.add("g", client.data.color.g);
                request.add("b", client.data.color.b);

                this.writeRequestToAll(request);

                this.game.nextPlayer();
                this.game.nextRound();

                if (this.game.ended)
                    this.game.giveScore();
            }
        }

        // client
        public void addClient(Client client)
        {

            this.clients.Add(client);
            client.setLobby(this);

            this.sendClientsToClients();

            if (this.clients.Count() >= Config.maxPlayersInGame)
                this.startGame();
        }

        public void removeClient(Client client)
        {

            this.clients.Remove(client);
            client.setLobby(null);

            this.sendClientsToClients();

            if (this.clients.Count() == 0)
                this.server.stopLobby(this);
        }

        private void sendClientsToClients()
        {

            Request request = Request.newRequest(Config.accountType);
            request.add("clients", this.getRequestClients());

            this.writeRequestToAll(request);
        }

        private List<Tuple<string, int[]>> getRequestClients()
        {

            List<Tuple<string, int[]>> clients = new List<Tuple<string, int[]>>();

            foreach (Client client in this.clients)
            {

                ClientColor color = client.data.color;
                clients.Add(new Tuple<string, int[]>(
                    client.data.name, 
                    new int[5] { color.r, color.g, color.b, client.data.score, client.data.wins }
                ));
            }

            return clients;
        }

        // request
        public void writeRequestToAll(Request request)
        {

            foreach (Client client in this.clients)
            {

                client.writeRequest(request);
            }
        }
    }
}
