using BKnE2Client.client.model;
using BKnE2Client.client.view;
using BKnE2Lib;
using BKnE2Lib.data;
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
            invokeFunction = new InvokeDelegate(InvokeFunction);
        }

        //Load the lobby when logged in
        private void OnLogin(Request obj)
        {
            LobbyForm lobby = new LobbyForm(controller);
            lobby.Show();
            controller.loginForm.Hide();
            controller.lobbyForm = lobby;
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
            List<string> players = (List<string>) obj.get("parameters");
            
            if(players != null)
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

        //Call the request.type function
        private void InvokeFunction(Request request)
        {
            functions[request.type].Invoke(request);
        }

        //Call the InvokeFunction delegate
        //Nessecairy to call the funciton on the application thread
        public override void receiveRequest(Request request)
        {
            controller.loginForm.Invoke(invokeFunction, request);
        }
    }
}
