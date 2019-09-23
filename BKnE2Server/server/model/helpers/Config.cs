using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.helpers
{

    static class Config
    {

        // connection
        public const string host          = "127.0.0.1";
        public const int port             = 42069;

        // game
        public const int maxPlayersInGame = 2;

        // paths
        public const string jsonpath      = @"C:\Users\wessel\Desktop\Avans\Periode 2.1\C#\BKnE2.0\BKnE2Server\server\model\json";

        // message presets
        public const string loginPreset   = "L";    // format server: L[name]:[password]
        public const string startPreset   = "S";    // format server: S
        public const string messagePreset = "M";    // format server: M[message]            format client: M[message]
        public const string pinPreset     = "P";    // format server: P[x]:[y]              format client: P[x]:[y]:[r,g,b]
        public const string gamePreset    = "G";    //                                      format client: G[name1]:[name2]:*   
    }
}
