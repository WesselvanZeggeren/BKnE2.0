using BKnE2Lib;
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
        private byte[] buffer;
        Encoding encoding = Encoding.UTF8;


        public AsyncConnection(SslStream stream, ClientConnection conn)
        {
            this.stream = stream;
            this.conn = conn;
            this.buffer = new byte[Config.maxMsgBytes];
            this.stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult result)
        {
            int receivedBytes = this.stream.EndRead(result);

            int offset = (int)Math.Ceiling((decimal)receivedBytes / 255);
            
            int messageLength = BitConverter.ToInt32(buffer, 0);
            byte[] totalBuffer = new byte[messageLength + Config.maxMsgBytes - offset];

            int totalRead = 0;

            while (totalRead < messageLength)
            {
                int read = stream.Read(totalBuffer, totalRead, totalBuffer.Length - totalRead);
                totalRead += read;
            }

            byte[] toReturn = new byte[messageLength];

            for (int i = 0; i < messageLength; i++)
            {
                toReturn[i] = totalBuffer[i + Config.maxMsgBytes - offset];
            }

            string message = Encoding.UTF8.GetString(toReturn);

            Request request = JsonConvert.DeserializeObject<Request>(message);

            this.conn.receiveRequest(request);
            this.stream.BeginRead(this.buffer, 0, buffer.Length, OnRead, null);
        }

        public void Write(Request request)
        {
            //The data to send
            byte[] serialisedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
            //The length of the message
            byte[] length = BitConverter.GetBytes(serialisedData.Length);
            byte[] toSend = new byte[Config.maxMsgBytes + serialisedData.Length];

            for (int i = 0; i < length.Length; i++)
            {
                toSend[i] = length[i];
            }
            //Write the data
            for (int i = 0; i < serialisedData.Length; i++)
            {
                toSend[i + Config.maxMsgBytes] = serialisedData[i];
            }

            stream.WriteAsync(toSend, 0, toSend.Length);
            stream.Flush();
        }
    }
}
