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
        private List<Client> nextRoundPlayers = new List<Client>();
        private List<Client> players;

        private int iterator = 0;

        public bool running = false;
        public int size;

        // constructor
        public Game(List<Client> players)
        {

            this.players = players;
        }

        // game
        public void init()
        {

            this.running = true;
            this.size = this.getSize(Config.minBoardSize);

            this.generatePins();
        }

        public void stop()
        {

        }

        // round
        public void nextRound()
        {

            if (this.playingPlayers() == 1)
            {

                this.currentPlayer().isPlaying = false;
                this.resetNextRoundPlayers();
                this.iterator = 0;

                if (this.nextRoundPlayers.Count() == 1)
                    this.stop();
                else
                    this.init();

                this.players = new List<Client>(this.newPlayerList());
                this.nextRoundPlayers = new List<Client>();
            }
        }

        // size
        private int getSize(int size)
        {

            if ((size * size) > (this.playingPlayers() * Config.maxPinsPerPlayer))
                return size;

            return this.getSize(size + 1);
        }

        // pins
        public bool receivePin(Client player, Request request)
        {

            Pin pin = this.getPin(request.get("x"), request.get("y"));

            if (player.data.id == this.currentPlayer().data.id && !pin.isAssigned)
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
                    this.pins.Add(new Pin(x, y));
        }

        // player
        private void resetNextRoundPlayers()
        {

            foreach (Client player in this.nextRoundPlayers)
                player.resetClient();
        }

        private int playingPlayers()
        {

            int playersPlaying = 0;

            foreach (Client player in this.players)
                if (player.isPlaying)
                    playersPlaying += 1;

            return playersPlaying;
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

            this.iterator += 1;

            if (this.iterator == this.players.Count())
                this.iterator = 0;

            if (!this.currentPlayer().isPlaying)
                this.nextPlayer();
        }

        private Client currentPlayer()
        {

            return this.players.ElementAt(this.iterator);
        }
    }
}
