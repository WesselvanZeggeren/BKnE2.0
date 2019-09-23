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

        // message presets
        public const string loginPreset   = "L";    // format: L[name]:[password]
        public const string messagePreset = "M";    // format: M[message]
        public const string startPreset   = "S";    // format: S
        public const string pinPreset     = "P";    // format: P[x]:[y]
    }
}
