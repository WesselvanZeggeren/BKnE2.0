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
        public string wins { get; set; }
        public string score { get; set; }
        public Color color { get; set; }

        public ClientData()
        {

        }
    }

    class Color
    {

        public int r { get; }
        public int g { get; }
        public int b { get; }

        public Color()
        {

        }
    }
}
