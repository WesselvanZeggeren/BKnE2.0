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

        public Controller()
        {
            this.connectionHandler = new ConnectionHandler(this);
            connectionHandler.startConnection();
        }

        public void Login(string name, string password, bool register)
        {
            Request login = Request.newRequest(Config.loginType);
            login.add("name", name);
            login.add("password", password);
            login.add("register", register);
            connectionHandler.writeRequest(login);
        }
    }
}
