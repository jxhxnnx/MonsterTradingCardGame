using System;
using Npgsql;

namespace MTCG
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Johanna's Server on Port 10001 :)");
            HTTPServer server = new HTTPServer(10001);
            server.Start();
        }
    }
}
