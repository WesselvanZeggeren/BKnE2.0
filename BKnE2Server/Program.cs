using BKnE2Server.server.controller;
using BKnE2Server.server.model.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2Server
{
    class Program
    {

        static void Main(string[] args)
        {

            Server server = new Server();
            server.startServer();

            //new Thread(new ThreadStart(new Test().start)).Start();

            Console.Read();
        }
    }
}
