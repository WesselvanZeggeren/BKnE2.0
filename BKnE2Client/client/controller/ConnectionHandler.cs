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
            functions = new Dictionary<string, Action<Request>>();
            functions[Config.loginType] = OnLogin;
            functions[Config.messageType] = OnMessage;
            functions[Config.pinType] = OnPin;
            functions[Config.accountType] = OnAccount;
            functions[Config.startType] = OnStart;
            invokeFunction = new InvokeDelegate(InvokeFunction);
        }
        
        //Load the lobby when logged in
        private void OnLogin(Request obj)
        {
            if (obj.get("successful"))
            {
                LobbyForm lobby = new LobbyForm(controller);
                lobby.Show();
                controller.loginForm.Hide();
                controller.lobbyForm = lobby;
                this.writeRequest(Request.newRequest(Config.lobbyType));
            }
        }

        //Adds a chat to the UI list
        private void OnMessage(Request obj)
        {
            if(controller.lobbyForm != null)
                controller.lobbyForm.AddChat(obj.get(Config.messageType));

            if (controller.gameForm != null)
                controller.gameForm.AddChat(obj.get(Config.messageType));
        }

        private void OnPin(Request obj)
        {
            controller.gameForm.SetPin(obj);
        }

        //Updates the list with players
        private void OnAccount(Request obj)
        {
            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(obj.get("players"));

            if (players != null)
            {
                controller.players = players;

                if (controller.lobbyForm != null)
                {
                    controller.lobbyForm.UpdatePlayerList();
                }
                if(controller.gameForm != null)
                {
                    controller.gameForm.UpdatePlayerList();
                }
            }
        }

        private void OnStart(Request obj)
        {
            if (obj.get("size") != null)
            {
                GameForm gameForm = new GameForm(controller);
                gameForm.Show();
                controller.lobbyForm.Hide();
                controller.gameForm = gameForm;
            }
            else
            {
                controller.gameForm.Hide();
                controller.lobbyForm.Show();
            }
        }

        //Call the request.type function
        private void InvokeFunction(Request request)
        {
            try
            {
                if(request != null)
                {
                    functions[request.type].Invoke(request);
                }
            } catch (Exception e)
            {
                if (controller.lobbyForm != null)
                {
                    controller.lobbyForm.SetServerMessage(DateTime.Now.Millisecond + " " + e.ToString());
                }
                if (controller.gameForm != null)
                {
                    controller.gameForm.SetServerMessage(DateTime.Now.Millisecond + " " + e.ToString());
                }
            }
        }

        //Call the InvokeFunction delegate
        //Nessecairy to call the funciton on the application thread
        public override void receiveRequest(Request request)
        {
            controller.loginForm.Invoke(invokeFunction, request);
        }
    }
}
