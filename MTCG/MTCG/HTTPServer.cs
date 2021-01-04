using System;
using System.Collections.Generic;
using System.IO;
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
        public List<string> user = new List<string>();


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
                // Client myclient = new Client(client);
                Console.WriteLine("Client connected successfully!");
                //Thread th = new Thread(new ParameterizedThreadStart(ClientHandler));
                //th.Start(myclient);
                //ClientHandler(client);
                //client.Close();
                ThreadPool.QueueUserWorkItem(ClientHandler, client);
            }

            running = false;
            listener.Stop();
        }

        public void ClientHandler(Object obj)
        //private void ClientHandler(TcpClient client)
        {          
                TcpClient newclient = (TcpClient)obj;
                //TcpClient newclient = (TcpClient)client;
                String message = "";
                StreamReader reader = new StreamReader(newclient.GetStream(), leaveOpen: true);
                while (reader.Peek() != -1)
                {
                    message += (char)reader.Read();
                }

                Requests request = new Requests(message);
                foreach (var x in request.Rest)
                {
                    Console.WriteLine(x.ToString());
                }

                MessageHandler msghandler = new MessageHandler(newclient, request.Type, request.Command, request.Authorization, request.Body, user);

                if (request.Type == "POST")
                {
                    if (request.Command == "/users")
                    {
                        msghandler.registerUser();
                    }
                    else if (request.Command == "/sessions")
                    {
                        user = msghandler.login(user);
                    }
                    else
                    {
                        msghandler.handlePost(user);
                    }
                }
                else
                {
                    msghandler.fromTypeToMethod(user);
                }
            
        }


    }
}
