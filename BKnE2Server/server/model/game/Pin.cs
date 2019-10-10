using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.game
{
    class Pin
    {

        public int x { get; }
        public int y { get; }

        public Pin (int x, int y)
        {

            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {

            return String.Format(
                "x: {0}\ty: {1}",
                this.x, this.y
            );
        }
    }
}
    