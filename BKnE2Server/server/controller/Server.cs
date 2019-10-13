using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Lib.helper;
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

        private List<Lobby> lobbys;
        
        // constructor
        public void startServer()
        {

            try
            {

                this.certificate = new X509Certificate2(Config.certificatePath, Config.certificateKey);
                this.lobbys = new List<Lobby>();

                new Thread(new ThreadStart(catchClients)).Start();
            }
            catch (Exception e)
            {

                ExceptionHelper.print(e);
            }
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

                ExceptionHelper.print(e);
                Thread.Sleep(10000);
                this.catchClients();
            }
        }

        // messaging
        public void receiveRequest(Client client, Request request)
        {
            Console.WriteLine("CLIENT: " + request.ToString());
            switch (request.type)
            {

                case Config.loginType:   client.login(request);                      break;
                case Config.pinType:     client.lobby.receivePin(client, request);   break;
                case Config.messageType: client.lobby.writeRequestToAll(request);    break;
                case Config.startType:   client.lobby.receiveStart(client, request); break;
                case Config.lobbyType:   this.addClientToLobby(client);              break;
            }
        }

        // game
        private Lobby findLobby()
        {

            foreach (Lobby lobby in this.lobbys)
                if (lobby.game == null)
                    return lobby;

            this.lobbys.Add(new Lobby(this));
            return this.findLobby();
        }

        public void addClientToLobby(Client client)
        {

            Lobby lobby = this.findLobby();

            lobby.addClient(client);
        }

        public void stopLobby(Lobby lobby)
        {

            this.lobbys.Remove(lobby);
        }
    }
}
