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
            client.color = ClientColor.generateColor();

            return client;
        }

        public override string ToString()
        {

            return String.Format(
                "id: {0}|name: {1}|password: {2}|wins: {3}|plays: {4}|score: {5}|color: {6}",
                this.id, this.name, this.password, this.wins, this.plays, this.score, this.color
            );
        }
    }
}
