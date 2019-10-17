using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.client
{
    class ClientData
    {

        public int id { get; set;  }
        public string name { get; set; }
        public string password { get; set; }
        public int wins { get; set; }
        public int plays { get; set; }
        public int score { get; set; }
        public ClientColor color { get; set; }

        public static ClientData newClient(int id, string name, string password)
        {

            ClientData client = new ClientData();

            client.id = id;
            client.name = name;
            client.password = password;
            client.wins = 0;
            client.plays = 0;
            client.score = 1000;
            client.color = ClientColor.generateColor();

            return client;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
