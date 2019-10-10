using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Server.server.model.client;
using BKnE2Server.server.model.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2Server.server.controller
{

    class Server
    {

        // attributes
        public X509Certificate2 certificate;

        private List<Game> games;
        
        // constructor
        public void startServer()
        {

            try
            {

                this.certificate = new X509Certificate2(Config.certificatePath, Config.certificateKey);
                this.games = new List<Game>();

                new Thread(new ThreadStart(catchClients)).Start();
            }
            catch (Exception e)
            {

                Console.WriteLine("Couldn't authenticate: {0}", e.StackTrace);

                if (e.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
            }

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
                listener.Start();

                while (true)
                {

                    Client client = new Client(this, listener.AcceptTcpClient());

                    Thread.Sleep(10);
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
        public void receiveRequest(Client client, Request request)
        {

            switch (request.type)
            {

                case Config.loginType:   client.login(request);                   break;
                case Config.startType:   client.game.startGame();                 break;
                case Config.pinType:     client.game.receivePin(client, request); break;
                case Config.messageType: client.game.writeRequestToAll(request);  break;
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

        public void addClientToGame(Client client)
        {

            Game game = this.findGame();

            game.addClient(client);
            client.game = game;
        }

        public void stopGame(Game game)
        {


        }
    }
}
