using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Base
{

    static class Config
    {

        // connection
        public const string host            = "127.0.0.1";
        public const int port               = 42069;

        // game
        public const int maxPlayersInGame   = 2;

        // message presets
        public const string startPreset     = "S";   // server: S                                    client: S
        public const string loginPreset     = "L";   // server: L[json{name, password, true/false}]  client: L[true/false]
        public const string messagePreset   = "M";   // server: M[message]                           client: M[message]
        public const string pinPreset       = "P";   // server: P[json{x, y}]                        client: P[json{x, y, {r, g, b}}]
        public const string accountPreset   = "A";   // server: A[json{r, g, b}]                     client: A[json{name1, name2, *}   

        // paths
        public static string accountPath    = getBasePath() + @"\BKnE2Server\server\model\account\Account.txt";

        private static string getBasePath()
        {

            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
    }
}
