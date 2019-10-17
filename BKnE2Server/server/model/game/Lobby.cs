using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
using Newtonsoft.Json;
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

            this.game = new Game(new List<Client>(this.clients));
            this.game.init();

            Request request = Request.newRequest(Config.startType);
            request.add("size", this.game.size);

            this.writeRequestToAll(request);
        }

        public void receiveStart(Client client, Request request)
        {

            if (request.get("start") && this.clients.Count() > 1)
                this.startGame();
            else if (this.game.ended) 
                this.removeClient(client);
        }

        // pin
        public void receivePin(Client client, Request request)
        {

            if (this.game.receivePin(client, request))
            {

                request.add("color", client.data.player.color.ToString());

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

            this.sendPlayersToClients();

            if (this.clients.Count() >= Config.maxPlayersInGame)
                this.startGame();
        }

        public void removeClient(Client client)
        {

            this.clients.Remove(client);
            client.setLobby(null);

            this.sendPlayersToClients();

            if (this.clients.Count() == 0)
                this.server.stopLobby(this);
        }

        private void sendPlayersToClients()
        {

            Request request = Request.newRequest(Config.playerType);
            request.add("players", JsonConvert.SerializeObject(this.getPlayers()));

            this.writeRequestToAll(request);
        }

        private List<Player> getPlayers()
        {

            List<Player> players = new List<Player>();

            foreach (Client client in this.clients)
                players.Add(client.data.player);

            return players;
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
