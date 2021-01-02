using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MTCG
{
    public class Client
    {
        public TcpClient client = new TcpClient();

        public StreamReader GetStreamReader()
        {
            return new StreamReader(client.GetStream());
        }

        public StreamWriter GetStreamWriter()
        {
            return new StreamWriter(client.GetStream()) { AutoFlush = true };
        }

        public void End()
        {
            client.Close();
        }
    }
}
