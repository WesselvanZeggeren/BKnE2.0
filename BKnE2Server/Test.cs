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

namespace BKnE2Server
{
    public class Test : Connection
    {

        public int sleep = 1000;

        public void start()
        {

            Thread.Sleep(sleep);

            this.startConnection();

            Thread.Sleep(sleep);

            this.loginRequest("admin", "admin", false);

            Thread.Sleep(sleep);

            this.lobbyRequest();

            Thread.Sleep(sleep);

            new Thread(new ThreadStart(new Test2().start2)).Start();

            Thread.Sleep(sleep * 4);

            this.pinRequest(0, 0);

            Thread.Sleep(sleep * 2);

            this.pinRequest(1, 0);

            Thread.Sleep(sleep * 2);

            this.pinRequest(2, 0);
        }

        public void loginRequest(string name, string password, bool register)
        {

            Request request = Request.newRequest(Config.loginType);
            request.add("name", name);
            request.add("password", password);
            request.add("register", register);

            Console.WriteLine("\n" + request + " :: SEND");

            this.writeRequest(request);
        }

        public void lobbyRequest()
        {

            Request request = Request.newRequest(Config.lobbyType);

            Console.WriteLine("\n" + request + " :: SEND");

            this.writeRequest(request);
        }

        public void messageRequest(string message)
        {

            Request request = Request.newRequest(Config.messageType);
            request.add("message", message);

            Console.WriteLine("\n" + request + " :: SEND");

            this.writeRequest(request);
        }

        public void pinRequest(int x, int y)
        {

            Request request = Request.newRequest(Config.pinType);
            request.add("x", x);
            request.add("y", y);

            Console.WriteLine("\n" + request + " :: SEND");

            this.writeRequest(request);
        }

        public override void receiveRequest(Request request)
        {

            Console.WriteLine(request + " :: RECEIVED");
        }
    }

    public class Test2 : Test
    {

        public bool relevant = true;

        public void start2()
        {

            Console.WriteLine();

            this.startConnection();

            Thread.Sleep(sleep);

            this.loginRequest("kees konders", "keesbeest", true);

            Thread.Sleep(sleep);

            this.relevant = false;

            this.lobbyRequest();

            Thread.Sleep(sleep);

            this.relevant = true;

            this.messageRequest("Hahahahha dit ontvangen we allebei");

            Thread.Sleep(sleep * 2);

            this.relevant = false;

            this.pinRequest(0, 1);

            Thread.Sleep(sleep * 2);

            this.pinRequest(1, 2);
        }

        public override void receiveRequest(Request request)
        {

            if (relevant)
            {

                base.receiveRequest(request);
            }
        }
    }

    public abstract class Connection
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

            this.thread = new Thread(new ThreadStart(readRequest));
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

        private void readRequest()
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
        public abstract void receiveRequest(Request request);
    }
}
