﻿using BKnE2Server.server.controller;
using BKnE2Server.server.model.client;
using BKnE2Server.server.model.json;
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

            //Server server = new Server();
            //server.startServer();

            Console.WriteLine("start");

            ClientData client = AccountManager.login("wessel", "0000", true );
            AccountManager.writeClients();
            client.color = ClientColor.newColor(10, 10, 10);
            AccountManager.save();

            Console.ReadLine();
            Console.WriteLine(client);
            Console.ReadLine();
        }
    }
}
