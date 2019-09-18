using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BKnE2._0.server.model.game;

namespace BKnE2._0.server.model.client
{

    class Client 
    {

        public int id;
        public string name;
        public List<Pin> pins;

        private TcpListener listener;
        private Thread thread;

        public Client(TcpListener listener)
        {

            this.thread = new Thread(new ThreadStart(handleClientConnetion));
            this.thread.Start();
        }

        public void handleClientConnection(TcpListener listener)
        {

        }
    }
}
