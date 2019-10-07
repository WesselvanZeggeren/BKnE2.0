using BKnE2Client.client.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.model
{
    class NetworkConnection
    {
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private Controller controller;
        private Encoding encoding = Encoding.UTF8;

        public NetworkConnection(Controller controller)
        {
            this.controller = controller;
        }
               
        //Connect to the server
        public void Connect(string ip, int port)
        {
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        //Read the message
        private void OnRead(IAsyncResult ar)
        {
            int receivedBytes = stream.EndRead(ar);
            string message = encoding.GetString(buffer, 0, receivedBytes);

            controller.HandleMessage(message);

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        //Send a message to the server
        public void Write(string message)
        {
            stream.Write(encoding.GetBytes(message), 0, message.Length);
            stream.Flush();
        }
    }
}
