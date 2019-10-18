using BKnE2Lib.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.helper
{
    public static class TCPHelper
    {

        public static Encoding encoding = Encoding.UTF8;

        // write
        public static string readText(SslStream stream)
        {

            StreamReader reader = new StreamReader(stream, encoding);
            return reader.ReadLine();
        }

        public static Request read(SslStream stream)
        {

            //Read the first EncryptionLayer.maxMsgLength bytes for the message length and convert it to an integer
            byte[] length = new byte[Config.maxMsgBytes];
            int amountRead = stream.Read(length, 0, Config.maxMsgBytes);
            int totalRead = 0;
            //Zet de length (Byte array) om naar een integer getal
            int messageLength = BitConverter.ToInt32(length, 0);
            //Maak een nieuwe buffer aan met de waarde van de message length
            byte[] buffer = new byte[messageLength + Config.maxMsgBytes - amountRead];

            //Zolang het ontvangen bericht kleiner is dan de buffer lengte
            while (totalRead < messageLength)
            {
                int read = stream.Read(buffer, totalRead, buffer.Length);
                totalRead += read;
            }

            byte[] toReturn = new byte[messageLength];

            for (int i = 0; i < messageLength; i++)
            {
                toReturn[i] = buffer[i + Config.maxMsgBytes - amountRead];
            }

            return JsonConvert.DeserializeObject<Request>(encoding.GetString(buffer, 0, totalRead));
        }

        // write
        public static void writeText(SslStream stream, string message)
        {

            StreamWriter writer = new StreamWriter(stream, encoding);
            writer.WriteLine(message);
            writer.Flush();
        }

        public static void write(SslStream stream, Request request)
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

            stream.Write(toSend, 0, toSend.Length);
            stream.Flush();
        }
    }
}
