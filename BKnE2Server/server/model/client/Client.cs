using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BKnE2Server.server.model.game;

namespace BKnE2Server.server.model.client
{

    class Client 
    {

        public int id;
        public string name;
        public List<Pin> pins;

        private TcpListener listener;
        private TcpClient client;
        private Thread thread;

        public Client(TcpListener listener)
        {

            this.listener = listener;

            this.thread = new Thread(new ThreadStart(handleClientConnection));
            this.thread.Start();
        }

        public void handleClientConnection()
        {


        }
    }
}
