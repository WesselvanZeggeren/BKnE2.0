using BKnE2._0.server.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2._0.server.model
{
    class ClientCatcher
    {

        private IObserver observer;

        public ClientCatcher(IObserver observer)
        {

            this.observer = observer;
        }

        public void start(string host, int port)
        {

            IPAddress ip = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint ep = new IPEndPoint(ip, port);

            try
            {



            }
            catch (Exception e)
            {

            }

            new Thread(new ThreadStart(catchClient)).Start();
        }

        private void catchClient()
        {

            while(true)
            {

            }
        }
    }
}
