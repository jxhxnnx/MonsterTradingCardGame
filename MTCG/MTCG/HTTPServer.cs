using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MTCG
{
    public class HTTPServer
    {
        public const String _version = "HTTP/1.1";
        private bool running = false;
        Dictionary<string, string> messages = new Dictionary<string, string>();
        private TcpListener listener;

        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private void Run()
        {
            running = true;
            listener.Start();

            while (running)
            {
                Console.WriteLine("Waiting for connections...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected successfully!");
                ClientHandler(client);
                client.Close();
            }

            running = false;
            listener.Stop();
        }

        private void ClientHandler(TcpClient client)
        {
            throw new NotImplementedException();
        }


    }
}
