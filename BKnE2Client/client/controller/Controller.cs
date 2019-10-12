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
        
        private ConnectionHandler connectionHandler;
                
        public LoginForm loginForm { get; set; }
        public LobbyForm lobbyForm { get; set; }
        public GameForm gameForm { get; set; }
        public ConnectionHandler ConnectionHandler { get => connectionHandler; set => connectionHandler = value; }
        public List<string> players;

        public Controller()
        {
            this.connectionHandler = new ConnectionHandler(this);
            connectionHandler.startConnection();
            players = new List<string>();
        }

        //Login to the server
        public void Login(string name, string password, bool register)
        {
            loginForm.loginName = name;
            Request login = Request.newRequest(Config.loginType);
            login.add("name", name);
            login.add("password", password);
            login.add("register", register);
            connectionHandler.writeRequest(login);
        }

        //Send a message to the server
        public void SendMessage(string msg)
        {
            int maxCharacters = 100;

            if (msg.Length > maxCharacters)
            {
                Stack<string> messages = new Stack<string>();

                for (int i = 0; i < msg.Length; i += maxCharacters)
                {
                    if (msg.Length - maxCharacters - i >= 0)
                    {
                        messages.Push(msg.Substring(i, maxCharacters) + "-");
                    }
                    else
                    {
                        messages.Push(msg.Substring(i, msg.Length - i));
                    }
                }

                int msgLength = messages.Count();
                for (int i = 0; i < msgLength; i++)
                {
                    WriteRequest(messages.Pop());
                }
            } else
            {
                WriteRequest(msg);
            }
        }

        private void WriteRequest(string msg)
        {
            Request message = Request.newRequest(Config.messageType);
            message.add(Config.messageType, msg);
            connectionHandler.writeRequest(message);
        }
    }
}
