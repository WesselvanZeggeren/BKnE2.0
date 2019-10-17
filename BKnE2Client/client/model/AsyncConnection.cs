using BKnE2Client.client.controller;
using BKnE2Lib.data;
using BKnE2Lib.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.model
{
    public static class AsyncConnection
    {
        private static Encoding encoding = Encoding.UTF8;
        private static SslStream sslStream;
        private static byte[] totalBuffer = new byte[1024];
        private static ClientConnection connection;

        public static void Connect(SslStream stream, ClientConnection conn)
        {
            sslStream = stream;
            connection = conn;
            sslStream.BeginRead(totalBuffer, 0, totalBuffer.Length, new AsyncCallback(OnRead), null);
        }

        //Sends a message to the connectionHandler when a message came in
        public static void OnRead(IAsyncResult ar)
        {
            int receivedBytes = sslStream.EndRead(ar);
            string message = encoding.GetString(totalBuffer, 1, totalBuffer.Length - 1);
            
            Request request = JsonConvert.DeserializeObject<Request>(message);
            connection.receiveRequest(request);
            sslStream.BeginRead(totalBuffer, 0, totalBuffer.Length, new AsyncCallback(OnRead), null);
        }

        public static async void Write(Request request)
        {
            byte[] messageBytes = encoding.GetBytes(JsonConvert.SerializeObject(request));
            byte[] bytes = new byte[messageBytes.Length + 1];

            bytes[0] = (byte)bytes.Length;

            for (int i = 0; i < messageBytes.Length; i++)
            {
                bytes[i + 1] = messageBytes[i];
            }

            await sslStream.WriteAsync(bytes, 0, bytes.Length);
            sslStream.Flush();
        }
    }
}
