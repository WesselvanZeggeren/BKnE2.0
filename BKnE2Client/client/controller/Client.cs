using BKnE2Client.client.model;
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

        public override void startClient()
        {

            this.startConnection();
        }

        public override void receiveRequest(Request request)
        {

            Console.WriteLine(request);
        }
    }
}
