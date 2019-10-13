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

            try
            {

                byte[] length = new byte[1];
                stream.Read(length, 0, 1);

                byte[] bytes = new byte[(int)length[0]];
                int readBytes = 1;

                while (readBytes < bytes.Length)
                    readBytes += stream.Read(bytes, readBytes, (bytes.Length - readBytes));

                return JsonConvert.DeserializeObject<Request>(encoding.GetString(bytes, 0, bytes.Length));
            }
            catch (Exception e)
            {

                throw e;
            }
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

            try
            {

                byte[] messageBytes = encoding.GetBytes(JsonConvert.SerializeObject(request));
                byte[] bytes = new byte[messageBytes.Length + 1];

                bytes[0] = (byte) bytes.Length;

                for (int i = 0; i < messageBytes.Length; i++)
                    bytes[i + 1] = messageBytes[i];

                stream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
