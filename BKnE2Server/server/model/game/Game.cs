using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
using BKnE2Server.server.model.json;
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
        private List<Client> nextRoundPlayers = new List<Client>();
        private List<Client> players;

        private int iterator = 0;

        public bool ended = false;
        public int size;

        // constructor
        public Game(List<Client> players)
        {

            this.players = players;
        }

        // game
        public void init()
        {

            this.size = this.getSize(Config.minBoardSize);

            this.generatePins();
        }

        // round
        public void nextRound()
        {

            if (this.playingPlayers().Count() == 1 && !this.ended)
            {

                this.currentPlayer().isPlaying = false;
                this.resetNextRoundPlayers();
                this.iterator = 0;

                if (this.nextRoundPlayers.Count() == 1)
                {

                    Console.WriteLine("GAME ENDED MOTHERFUCKER!1111!111!11!!!!111!11!!11111!!111!!!");

                    this.ended = true;
                }
                else
                {

                    this.init();

                    this.players = new List<Client>(this.newPlayerList());
                    this.nextRoundPlayers = new List<Client>();
                }
            }
        }

        // size
        private int getSize(int size)
        {

            if ((size * size) > (this.playingPlayers().Count() * Config.maxPinsPerPlayer))
                return size;

            return this.getSize(size + 1);
        }

        // pins
        public bool receivePin(Client player, Request request)
        {

            Pin pin = this.getPin((int) request.get("x"), (int) request.get("y"));

            if (player.data.id == this.currentPlayer().data.id && !pin.isAssigned && !this.ended)
            {

                pin.isAssigned = true;
                player.assignPin(pin);

                if (player.hasThreeInARow())
                    this.nextRoundPlayers.Add(player);

                return true;
            }

            return false;
        }

        private Pin getPin(int x, int y)
        {

            foreach (Pin pin in this.pins)
                if (pin.x == x && pin.y == y)
                    return pin;

            return null;
        }

        private void generatePins()
        {

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    this.pins.Add(Pin.newPin(x, y));
        }

        // player
        private void resetNextRoundPlayers()
        {

            foreach (Client player in this.nextRoundPlayers)
                player.resetClient();
        }

        private List<Client> playingPlayers()
        {

            List<Client> playingPlayers = new List<Client>();

            foreach (Client player in this.players)
                if (player.isPlaying)
                    playingPlayers.Add(player) ;

            return playingPlayers;
        }

        private List<Client> newPlayerList()
        {

            foreach (Client player in this.players)
                if (!player.isPlaying)
                    this.nextRoundPlayers.Add(player);

            return this.nextRoundPlayers;
        }

        public void nextPlayer()
        {

            if (!this.ended)
            {

                this.iterator += 1;

                if (this.iterator == this.players.Count())
                    this.iterator = 0;

                if (!this.currentPlayer().isPlaying)
                    this.nextPlayer();
            }
        }

        private Client currentPlayer()
        {

            return this.players.ElementAt(this.iterator);
        }

        // score
        public void giveScore()
        {

            Client winner = this.nextRoundPlayers.ElementAt(0);
            this.players.Remove(winner);

            winner.data.player.wins += 1;
            winner.data.player.plays += 1;
            winner.data.player.score += Config.maxScorePerGame;

            int scorePerPlayer = Config.maxScorePerGame / ((this.players.Count()) / 2);

            for (int i = 0; i < this.players.Count(); i++)
            {

                Client player = this.players.ElementAt(i);

                player.data.player.plays += 1;
                player.data.player.score += Config.maxScorePerGame - (scorePerPlayer * (i + 1));
            }

            AccountManager.save();

            Request request = Request.newRequest(Config.messageType);
            request.add("message", $"Congragulations {winner.data.name}");

            winner.lobby.writeRequestToAll(request);
        }
    }
}
