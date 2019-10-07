using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.controller
{
    class MessageHandler
    {
        private Controller controller;
        Dictionary<string, Action<string>> functions;

        public MessageHandler(Controller controller)
        {
            this.controller = controller;
            functions = new Dictionary<string, Action<string>>();
            functions[ClientConfig.startPreset] = OnStart;
            functions[ClientConfig.loginPreset] = OnLogin;
            functions[ClientConfig.messagePreset] = OnMessage;
            functions[ClientConfig.pinPreset] = OnPin;
            functions[ClientConfig.NamePreset] = OnName;
        }

        private void OnStart(string obj)
        {
            throw new NotImplementedException();
        }

        private void OnLogin(string obj)
        {
            throw new NotImplementedException();
        }

        private void OnMessage(string obj)
        {
            throw new NotImplementedException();
        }

        private void OnPin(string obj)
        {
            throw new NotImplementedException();
        }

        private void OnName(string obj)
        {
            throw new NotImplementedException();
        }

        //Call the function by the message preset
        public void Invoke(string message)
        {
            functions[message.Substring(0,1)].Invoke(message);
        }
    }
}
