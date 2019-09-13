using BKnE2._0.server.model;
using BKnE2._0.server.model.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2._0.server.controller
{

    class Server : IObserver
    {

        private List<Game> games;
        
        public Server(string host, int port)
        {

            this.games = new List<Game>();

            new ClientCatcher(this).start(host, port);
        }

        public void receiveClient(Client client)
        {

            this.findGame().addClient(client);
        }

        private Game findGame()
        {

            foreach (Game game in this.games)
                if (!game.isRunning())
                    return game;

            this.games.Add(new Game());
            return this.findGame();
        }

        public void stopGame(Game game)
        {


        }
    }
}
