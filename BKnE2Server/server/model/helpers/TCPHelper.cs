using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Server.server.model.helpers
{
    class TCPHelper
    {

        public static Encoding encoding = Encoding.UTF8;

        public static string readText(NetworkStream networkStream)
        {

            StreamReader stream = new StreamReader(networkStream, encoding);
            return stream.ReadLine();
        }

        public static void sendText(NetworkStream networkStream, string message)
        {

            StreamWriter stream = new StreamWriter(networkStream, encoding);
            stream.WriteLine(message);
            stream.Flush();
        }
    }
}
