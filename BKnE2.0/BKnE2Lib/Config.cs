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
         * Start type. This type starts the game. It is used to start and quit a game or to recieve its size
         * Server params:   "start":true
         * Client params:   "size":3
         */
        public const string startType = "start";

        /**
         * Lobby type. This type joins a lobby. It requires no parameters from the server and client side.
         */
        public const string lobbyType = "lobby";

        /**
         * Login type. This type is used to log a user in and confirm if the login was succesfull.
         * Server params:   "name":"admin", "password":"admin", "register": true
         * Client params:   "successful":true
         */
        public const string loginType = "login";

        /**
         * Message type. This type is used to deliver messages to another client.
         * Server params:   "message":"Hello World To Server!"
         * Client params:   "message":"Hello World Form Server!"
         */
        public const string messageType = "message";

        /**
         * Pin type. This type is used to let players click pins and receive one if it is successfully selected.
         * Server params:   "x":1, "y":3
         * Client params:   "x":1, "y":3, "color":Color
         */
        public const string pinType = "pin";

        /**
         * Player type: This type is used to change the players color or show wich players are in a room.
         * Server params:   "r":255, "g":0, "b":0
         * Client params:   "players":List<Player>
         */
        public const string playerType = "player";

        // connection
        public const string machineName = "BKnE2Server";
        public const string host = "127.0.0.1";
        public const int port = 6699;

        public const int connectionTimeout = 3600000;

        // game
        public const int maxPinsPerPlayer = 4;
        public const int maxPlayersInGame = 2; // when Battle Royale this should become 20
        public const int maxScorePerGame = 50;
        public const int startScore = 1000;
        public const int minBoardSize = 3;

        // account
        public const int maxAmmountOfAccounts = 100;

        // paths
        public static string certificateKey = getBasePath() + @"\BKnE2Server\server\certificate\cert.key";
        public static string certificatePath = getBasePath() + @"\BKnE2Server\server\certificate\cert.crt";
        public static string accountPath = getBasePath() + @"\BKnE2Server\server\model\account\Account.txt";

        private static string getBasePath()
        {

            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
    }
}
