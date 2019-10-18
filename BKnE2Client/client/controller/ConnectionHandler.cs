using BKnE2Client.client.model;
using BKnE2Client.client.view;
using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Lib.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BKnE2Client.client.controller
{
    public class ConnectionHandler : ClientConnection
    {
        private Controller controller;
        private Dictionary<string, Action<Request>> functions;
        private delegate void InvokeDelegate(Request r);
        private InvokeDelegate invokeFunction;

        public ConnectionHandler(Controller controller)
        {

            this.controller = controller;

            this.functions = new Dictionary<string, Action<Request>>();
            this.functions[Config.loginType] = OnLogin;
            this.functions[Config.messageType] = OnMessage;
            this.functions[Config.pinType] = OnPin;
            this.functions[Config.playerType] = OnPlayer;
            this.functions[Config.startType] = OnStart;

            this.invokeFunction = new InvokeDelegate(InvokeFunction);
        }
        
        //Load the lobby when logged in
        private void OnLogin(Request obj)
        {

            if (obj.get("successful"))
            {

                LobbyForm lobby = new LobbyForm(controller);
                lobby.Show();

                this.controller.loginForm.Hide();
                this.controller.lobbyForm = lobby;

                this.writeRequest(Request.newRequest(Config.lobbyType));
            }
        }

        //Adds a chat to the UI list
        private void OnMessage(Request obj)
        {

            if (this.controller.lobbyForm != null)
                this.controller.lobbyForm.AddChat(obj.get(Config.messageType));

            if (this.controller.gameForm != null)
                this.controller.gameForm.AddChat(obj.get(Config.messageType));
        }

        private void OnPin(Request obj)
        {

            this.controller.gameForm.SetPin(obj);
        }

        //Updates the list with players
        private void OnPlayer(Request obj)
        {

            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(obj.get("players"));

            if (players != null)
            {

                this.controller.players = players;

                if (this.controller.lobbyForm != null)
                    this.controller.lobbyForm.UpdatePlayerList();

                if (this.controller.gameForm != null)
                    this.controller.gameForm.UpdatePlayerList();
            }
        }

        private void OnStart(Request obj)
        {

            if (obj.get("size") != null)
            {

                GameForm gameForm = new GameForm(controller);
                gameForm.Show();

                this.controller.lobbyForm.Hide();
                this.controller.gameForm = gameForm;
            }
            else
            {

                this.controller.gameForm.Hide();
                this.controller.lobbyForm.Show();
            }
        }

        //Call the request.type function
        private void InvokeFunction(Request request)
        {

            try
            {

                if (request != null)
                    this.functions[request.type].Invoke(request);
            }
            catch (Exception e)
            {

                if (this.controller.lobbyForm != null)
                    this.controller.lobbyForm.SetServerMessage(DateTime.Now.Millisecond + " " + e.ToString());

                if (this.controller.gameForm != null)
                    this.controller.gameForm.SetServerMessage(DateTime.Now.Millisecond + " " + e.ToString());
            }
        }

        //Call the InvokeFunction delegate
        //Nessecairy to call the funciton on the application thread
        public override void receiveRequest(Request request)
        {

            this.controller.loginForm.Invoke(invokeFunction, request);
        }
    }
}
