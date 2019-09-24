using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.client
{
    class ClientData
    {

        public int id { get; }
        public string name { get; }
        public string password { get; }
        public int wins { get; set; }
        public int score { get; set; }
        public Color color { get; set; }

        public ClientData(int id, string name, string password)
        {

            this.id = id;
            this.name = name;
            this.password = password;

            this.wins = 0;
            this.score = 0;
            this.color = new Color();
        }
    }

    class Color
    {

        public int r { get; }
        public int g { get; }
        public int b { get; }

        public Color()
        {

            Random random = new Random();

            this.r = random.Next(256);
            this.g = random.Next(256);
            this.b = random.Next(256);
        }
    }
}
