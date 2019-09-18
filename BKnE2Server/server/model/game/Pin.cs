using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.game
{
    class Pin
    {

        public uint x { get; }
        public uint y { get; }

        public Pin (uint x, uint y)
        {

            this.x = x;
            this.y = y;
        }
    }
}
