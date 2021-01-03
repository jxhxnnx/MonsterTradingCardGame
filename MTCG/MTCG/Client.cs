﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MTCG
{
    public class Client
    {
        public TcpClient client;
        public Client(TcpClient _client)
        {
            this.client = _client;
        }
        
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
