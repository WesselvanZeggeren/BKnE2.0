﻿using Newtonsoft.Json;
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
        public bool isAssigned { get; set; }

        public Pin (int x, int y)
        {

            this.x = x;
            this.y = y;
            this.isAssigned = false;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
    