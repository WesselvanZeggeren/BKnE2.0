using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib
{

    public static class Config
    {

        // connection
        public const string machineName      = "BKnE2Server";
        public const string host             = "127.0.0.1";
        public const int port                = 6699;

        // game
        public const int maxPlayersInGame    = 2;

        // request types
        public const string startType        = "start";   // server: S                                    client: S
        public const string loginType        = "login";   // server: L[json{name, password, true/false}]  client: L[true/false]
        public const string messageType      = "message"; // server: M[message]                           client: M[message]
        public const string pinType          = "pin";     // server: P[json{x, y}]                        client: P[json{x, y, {r, g, b}}]
        public const string accountType      = "account"; // server: A[json{r, g, b}]                     client: A[json{name1, name2, *}   

        // paths
        public static string certificateKey  = getBasePath() + @"\BKnE2Server\server\certificate\cert.key";
        public static string certificatePath = getBasePath() + @"\BKnE2Server\server\certificate\cert.crt";
        public static string accountPath     = getBasePath() + @"\BKnE2Server\server\model\account\Account.txt";

        private static string getBasePath()
        {

            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
    }
}
