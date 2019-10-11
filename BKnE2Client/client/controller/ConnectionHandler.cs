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
    class ConnectionHandler : ClientConnection
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

        private void OnLogin(Request obj)
        {
            Lobby lobby = new Lobby(controller);
            lobby.Show();
            controller.form.Hide();
            controller.form = lobby;
        }

        private void OnMessage(Request obj)
        {
            
        }

        private void OnPin(Request obj)
        {
            
        }

        private void OnAccount(Request obj)
        {
            
        }

        private void InvokeFunction(Request request)
        {
            functions[request.type].Invoke(request);
        }

        //Call the function in the dictionairy
        public override void receiveRequest(Request request)
        {
            controller.form.Invoke(invokeFunction, request);
        }
    }
}
