using BKnE2Base;
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

        // attributes
        private List<Game> games;
        
        // constructor
        public void startServer()
        {

            this.games = new List<Game>();

            new Thread(new ThreadStart(catchClients)).Start();

            Console.Read();
        }

        // connection
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

                    TcpClient tcpClient = listener.AcceptTcpClient();

                    Client client = new Client(this, tcpClient);
                    Game game = this.findGame();

                    game.addClient(client);
                    client.game = game;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(String.Format("Client Catcher chrashed.\n{0}.\nIt will restart in 10 seconds!", e.StackTrace));
                Thread.Sleep(10000);
                this.catchClients();
            }
        }

        // messaging
        public void receiveMessage(Client client, string receivedMessage)
        {

            string preset = receivedMessage.Substring(0, 1);
            string message = receivedMessage.Substring(1);

            switch (preset)
            {

                case Config.loginPreset:   client.login(message);                   break;
                case Config.startPreset:   client.game.startGame();                 break;
                case Config.pinPreset:     client.game.receivePin(client, message); break;
                case Config.messagePreset: client.game.sendAll(message);            break;
            } 
        }

        // game
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
