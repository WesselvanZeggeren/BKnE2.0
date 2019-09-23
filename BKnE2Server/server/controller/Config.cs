using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.controller
{

    static class Config
    {

        // connection
        public const string host          = "127.0.0.1";
        public const int port             = 42069;

        // game
        public const int maxPlayersInGame = 2;

        // presets
        public const string loginPreset   = "L";
        public const string messagePreset = "M";
        public const string startPreset   = "S";
        public const string pinPreset     = "P";
    }
}
