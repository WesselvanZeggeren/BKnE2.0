using BKnE2Server.server.model.client;
using BKnE2Server.server.model.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2Server.server.controller
{

    class Server
    {

        private List<Game> games;
        
        public void startServer()
        {

            this.games = new List<Game>();

            new Thread(new ThreadStart(catchClients)).Start();
        }

        private void catchClients()
        {

            try
            {

                IPAddress ip;

                if (!IPAddress.TryParse(Config.host, out ip))
                    throw new Exception();

                TcpListener listener = new TcpListener(ip, Config.port);

                while (true)
                {

                    this.receiveClient(new Client(listener));
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(String.Format("Client Catcher chrashed.\n{0}.\nIt will restart in 10 seconds!", e.StackTrace));
                Thread.Sleep(10000);
                this.catchClients();
            }
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

            this.games.Add(new Game(this));
            return this.findGame();
        }

        public void stopGame(Game game)
        {


        }
    }
}
