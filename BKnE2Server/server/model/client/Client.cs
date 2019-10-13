using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BKnE2Server.server.model.game;
using BKnE2Server.server.controller;
using BKnE2Server.server.model.json;
using Newtonsoft.Json;
using System.Collections;
using BKnE2Lib.helper;
using BKnE2Lib;
using System.Net.Security;
using BKnE2Lib.data;
using System.Security.Authentication;

namespace BKnE2Server.server.model.client
{

    class Client
    {

        // attributes
        public ClientData data = null;
        public bool isPlaying = true;

        public Lobby lobby;
        private List<Pin> pins;

        private Server server;
        private Thread thread;
        private SslStream stream;

        // constructor
        public Client(Server server, TcpClient client)
        {

            this.server = server;

            this.pins = new List<Pin>();

            this.thread = new Thread(handleClientConnection);
            this.thread.Start(client);
        }

        // connection
        private void handleClientConnection(object obj)
        {

            TcpClient client = obj as TcpClient;
            this.stream = new SslStream(client.GetStream(), false);

            try
            {

                this.stream.AuthenticateAsServer(this.server.certificate, false, SslProtocols.Tls, true);

                SSLHelper.DisplaySecurityLevel(this.stream);
                SSLHelper.DisplaySecurityServices(this.stream);
                SSLHelper.DisplayCertificateInformation(this.stream);
                SSLHelper.DisplayStreamProperties(this.stream);

                this.stream.ReadTimeout = Config.connectionTimeout;
                this.stream.WriteTimeout = Config.connectionTimeout;

                while (true)
                {

                    this.server.receiveRequest(this, TCPHelper.read(this.stream));

                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}\n name: {1}", e.Message, e.GetType().Name);

                if (e.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
            }
            finally
            {

                this.stream.Close();
                client.Close();
            }
        }

        public void writeRequest(Request request)
        {

            TCPHelper.write(this.stream, request);
        }

        // account
        public void login(Request request)
        {

            this.data = AccountManager.login(request.get("name"), request.get("password"), request.get("register"));

            request.clear();
            request.add("successful", (this.data != null));

            this.writeRequest(request);
        }

        // pins
        public void assignPin(Pin pin)
        {

            if (this.pins.Count() == 4)
                this.pins.RemoveAt(0);

            this.pins.Add(pin);
        }

        private bool containsPin(int x, int y)
        {

            foreach (Pin pin in this.pins)
                if (pin.x == x && pin.y == y)
                    return true;

            return false;
        }

        // game
        public bool hasThreeInARow()
        {

            if (this.isPlaying)
                foreach (Pin p in this.pins)
                    if (this.containsPin(p.x    , p.y + 1) && this.containsPin(p.x    , p.y + 2) ||
                        this.containsPin(p.x + 1, p.y + 1) && this.containsPin(p.x + 2, p.y + 2) ||
                        this.containsPin(p.x + 1, p.y    ) && this.containsPin(p.x + 2, p.y    ) ||
                        this.containsPin(p.x + 1, p.y - 1) && this.containsPin(p.x + 2, p.y - 2))
                        this.isPlaying = false;

            return this.isPlaying;
        }

        public void resetClient()
        {

            this.pins = new List<Pin>();
            this.isPlaying = true;
        }

        // lobby
        public void setLobby(Lobby lobby)
        {

            if (this.lobby == null || lobby == null)
                this.lobby = lobby;
        }
    }
}
