using BKnE2Lib.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Client.client.model
{
    public class AsyncConnection
    {
        private SslStream stream;
        private ClientConnection conn;
        private byte[] buffer = new byte[1];
        Encoding encoding = Encoding.UTF8;


        public AsyncConnection(SslStream stream, ClientConnection conn)
        {
            this.stream = stream;
            this.conn = conn;
            this.stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult ar)
        {
            try
            {
                int receivedBytes = this.stream.EndRead(ar);
                int packetLength = (int)buffer[0]; //BitConverter.ToInt32(buffer, 0);
                byte[] totalBuffer = new byte[packetLength];
                int readPos = 1;

                while (readPos < packetLength)
                {
                    receivedBytes = stream.Read(totalBuffer, readPos, packetLength - readPos);
                    readPos += receivedBytes;
                }

                byte[] toReturn = new byte[totalBuffer.Length - 1];

                for(int i = 0; i < toReturn.Length; i++)
                {
                    toReturn[i] = totalBuffer[i + 1];
                }

                string message = Encoding.UTF8.GetString(toReturn);

                Request request = JsonConvert.DeserializeObject<Request>(message);

                this.conn.receiveRequest(request);
                this.stream.BeginRead(this.buffer, 0, buffer.Length, OnRead, null);
            }
            catch (IOException)
            {
                stream.Close();
            }
        }

        public void Write(Request request)
        {
            try
            {
                byte[] messageBytes = encoding.GetBytes(JsonConvert.SerializeObject(request));
                byte[] bytes = new byte[messageBytes.Length + 1];

                bytes[0] = (byte)bytes.Length;

                for (int i = 0; i < messageBytes.Length; i++)
                    bytes[i + 1] = messageBytes[i];

                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            catch (ObjectDisposedException)
            {
                
            }
        }
    }
}
