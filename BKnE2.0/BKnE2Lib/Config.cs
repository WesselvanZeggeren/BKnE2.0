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

        /**
         * Start type. This type starts the game. It does not require any parameters from the server and the client
         */
        public const string startType = "start";

        /**
         * Login type. This type is used to log a user in and confirm if the login was succesfull.
         * Server params:   "name":"admin", "password":"admin", "register": true
         * Client params:   "successful":true
         */
        public const string loginType = "login";

        /**
         * Message type. This type is used to deliver messages to another client.
         * Server params:   "message":"Hello World!"
         * Client params:   "message":"Hello World!"
         */
        public const string messageType = "message";

        /**
         * Pin type. This type is used to let players click pins and receive one if it is successfully selected.
         * Server params:   "x":1, "y":3
         * Client params:   "x":1, "y":3, "r":255, "g":0, "b":0
         */
        public const string pinType = "pin";

        /**
         * Account type: This type is used to change the players color or show wich players are in a room.
         * Server params:   "r":255, "g":0, "b":0
         * Client params:   "names":List<string> {"kees", "bert", "joost"} <<<<<<<< COULD|BECOME|DIFFERENT
         */
        public const string accountType = "account";



        // connection
        public const string machineName      = "BKnE2Server";
        public const string host             = "127.0.0.1";
        public const int port                = 6699;

        // game
        public const int maxPlayersInGame    = 2;

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
