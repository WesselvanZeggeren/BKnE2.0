using BKnE2Client.client.model;
using BKnE2Client.client.view;
using BKnE2Lib;
using BKnE2Lib.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKnE2Client.client.controller
{
    public class Controller
    {

        public ConnectionHandler ConnectionHandler { get => connectionHandler; set => connectionHandler = value; }
        private ConnectionHandler connectionHandler;

        public LoginForm loginForm { get; set; }
        public LobbyForm lobbyForm { get; set; }
        public GameForm gameForm { get; set; }

        public List<Player> players;
        public ListBox.ObjectCollection messages;

        public Controller()
        {

            this.connectionHandler = new ConnectionHandler(this);
            this.connectionHandler.startConnection();

            this.players = new List<Player>();
        }

        //Login to the server
        public void Login(string name, string password, bool register)
        {

            this.loginForm.loginName = name;

            Request login = Request.newRequest(Config.loginType);
            login.add("name", name);
            login.add("password", password);
            login.add("register", register);

            this.connectionHandler.writeRequest(login);
        }

        public void GameRequest(bool startGame)
        {

            Request request = Request.newRequest(Config.startType);
            request.add("start", startGame);

            this.connectionHandler.writeRequest(request);
        }

        //Send a message to the server
        public void SendMessage(string name, string msg)
        {

            int maxCharacters = 100;
            string namedMsg = name + ": " + msg;

            if (namedMsg.Length > maxCharacters)
            {

                Stack<string> messages = new Stack<string>();

                for (int i = 0; i < namedMsg.Length; i += maxCharacters)
                    if (namedMsg.Length - maxCharacters - i >= 0)
                        messages.Push(namedMsg.Substring(i, maxCharacters) + "-");
                    else
                        messages.Push(namedMsg.Substring(i, namedMsg.Length - i));

                int msgLength = messages.Count();

                for (int i = 0; i < msgLength; i++)
                    WriteRequest(messages.Pop());
            }
            else
                WriteRequest(namedMsg);

            if (msg == "quit")
            {

                Request request = Request.newRequest(Config.startType);
                request.add("start", false);

                this.connectionHandler.writeRequest(request);
            }
        }

        //Send a pin to the server
        public void SendPin(int x, int y)
        {

            Request pinRequest = Request.newRequest(Config.pinType);
            pinRequest.add("x", x);
            pinRequest.add("y", y);

            this.connectionHandler.writeRequest(pinRequest);
        }

        private void WriteRequest(string msg)
        {

            Request message = Request.newRequest(Config.messageType);
            message.add(Config.messageType, msg);

            this.connectionHandler.writeRequest(message);
        }
    }
}
