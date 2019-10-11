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
                
        public Login login { get; set; }
        public Lobby lobby { get; set; }
        public Game game { get; set; }
        public ConnectionHandler ConnectionHandler { get => connectionHandler; set => connectionHandler = value; }

        public Controller()
        {
            this.connectionHandler = new ConnectionHandler(this);
            connectionHandler.startConnection();
        }

        //Login to the server
        public void Login(string name, string password, bool register)
        {
            Request login = Request.newRequest(Config.loginType);
            login.add("name", name);
            login.add("password", password);
            login.add("register", register);
            connectionHandler.writeRequest(login);
        }

        //Send a message to the server
        public void SendMessage(string msg)
        {
            Request message = Request.newRequest(Config.messageType);
            message.add(Config.messageType, msg);
            connectionHandler.writeRequest(message);
        }
    }
}
