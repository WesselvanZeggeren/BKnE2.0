using BKnE2Lib;
using BKnE2Lib.data;
using BKnE2Lib.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKnE2Client.client.model
{
    public abstract class ClientConnection
    {

        private SslStream stream;
        private Thread thread;

        // connection
        protected void startConnection()
        {

            TcpClient client = new TcpClient(Config.host, Config.port);
            this.stream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(validateCertificate), null);

            try
            {

                this.stream.AuthenticateAsClient(Config.machineName);

                TCPHelper.write(this.stream, Request.newRequest());
            }
            catch (Exception e)
            {

                Console.WriteLine("Couldn't authenticate: {0}", e.StackTrace);

                if (e.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);

                client.Close();
                return;
            }

            this.thread = new Thread(new ThreadStart(getMessage));
            this.thread.Start();
        }

        private bool validateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate errors: {0}", sslPolicyErrors);

            return false;
        }

        // messaging
        protected void writeRequest(Request request)
        {

            TCPHelper.write(this.stream, request);
        }

        private void getMessage()
        {

            try
            {

                while (true)
                {

                    this.receiveRequest(TCPHelper.read(this.stream));

                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Message Receiver crasched : {0}", e.ToString());
                this.stream = null;
                this.thread = null;
            }
        }

        // overrides
        public abstract void startClient();
        public abstract void receiveRequest(Request request);
    }
}
