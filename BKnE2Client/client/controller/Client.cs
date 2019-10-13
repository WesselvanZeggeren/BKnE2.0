using BKnE2Client.client.model;
using BKnE2Lib;
using BKnE2Lib.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.controller
{
    class Client : ClientConnection
    {

        public void startClient()
        {

            this.startConnection();

            Request request = Request.newRequest(Config.loginType);
            request.add("name", "admin");
            request.add("password", "admin");
            request.add("register", true);

            this.writeRequest(request);

            request = Request.newRequest(Config.messageType);
            request.add("message", "Dit Werkt!");

            this.writeRequest(request);
        }

        public override void receiveRequest(Request request)
        {

            switch (request.type)
            {

                case Config.loginType: Console.WriteLine((request.get("successful")) ? "it logged in" : "it failed"); break;
                case Config.messageType: Console.WriteLine(request.get("message")); break;
                //case Config.startType: break;
                //case Config.pinType: client.game.receivePin(client, request); break;
            }
        }
    }
}
