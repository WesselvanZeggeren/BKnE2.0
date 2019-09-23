using BKnE2Server.server.model.client;
using BKnE2Server.server.model.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.json
{

    static class JSONHelper
    {

        public static ClientData login(string name, string password)
        {

            if (!File.Exists(Config.jsonPath))

            ClientData data = new ClientData();

            return data;
        }

        public static void save(ClientData data)
        {


        }
    }
}
