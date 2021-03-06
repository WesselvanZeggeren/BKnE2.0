﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client
{
    static class ClientConfig
    {
        // connection
        public const string host = "127.0.0.1";
        public const int port = 42069;

        // game
        public const int maxPlayersInGame = 2;

        // paths
        public const string jsonPath = "Accounts.txt";

        // message presets
        public const string startPreset = "S";   // server: S
        public const string loginPreset = "L";   // server: L[name]:[password]:[true/false]    client: L[true/false]
        public const string messagePreset = "M";   // server: M[message]                         client: M[message]
        public const string pinPreset = "P";   // server: P[x]:[y]                           client: P[x]:[y]:[r,g,b]
        public const string NamePreset = "N";   //                                            client: N[name1]:[name2]:*   
    }
}
