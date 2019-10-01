using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Base
{
    class TCPHelper
    {

        public static Encoding encoding = Encoding.UTF8;

        // write
        public static string readText(NetworkStream networkStream)
        {

            StreamReader stream = new StreamReader(networkStream, encoding);
            return stream.ReadLine();
        }

        public static string read(NetworkStream stream)
        {

            try
            {

                byte[] length = new byte[1];
                stream.Read(length, 0, 1);

                byte[] bytes = new byte[(int)length[0]];
                int readBytes = 1;

                while (readBytes < bytes.Length)
                    readBytes += stream.Read(bytes, readBytes, (bytes.Length - readBytes));

                return encoding.GetString(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // send
        public static void sendText(NetworkStream networkStream, string message)
        {

            StreamWriter stream = new StreamWriter(networkStream, encoding);
            stream.WriteLine(message);
            stream.Flush();
        }

        public static void send(NetworkStream stream, string message)
        {

            try
            {

                byte[] messageBytes = encoding.GetBytes(message);
                byte[] bytes = new byte[messageBytes.Length + 1];

                bytes[0] = (byte)messageBytes.Length;

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
