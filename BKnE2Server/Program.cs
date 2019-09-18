using BKnE2Server.server.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server
{
    class Program
    {
        static void Main(string[] args)
        {

            Server server = new Server();
            server.startServer();
        }
    }
}
