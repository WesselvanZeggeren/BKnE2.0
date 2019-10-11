using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.client
{
    class ClientColor
    {

        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public static ClientColor newColor(int r, int g, int b)
        {

            ClientColor color = new ClientColor();

            color.r = r;
            color.g = g;
            color.b = b;

            return color;
        }

        public static ClientColor generateColor()
        {

            Random random = new Random();

            return ClientColor.newColor(random.Next(256), random.Next(256), random.Next(256));
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
