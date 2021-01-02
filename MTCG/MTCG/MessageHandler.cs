using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MTCG
{
    public class MessageHandler
    {
        public TcpClient client;
        public string type;
        public string order;
        public string authorization;
        public string body;
        public List<string> user;
        DB db;

        public MessageHandler(TcpClient _client, string _type, string _order, string _authorization, string _body, List<string> _user)
        {
            client = _client;
            type = _type;
            order = _order;
            authorization = _authorization;
            body = _body;
            db = new DB();
            user = _user;
        }

        public void Response(string status, string mime, string data)
        {
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            int dataLength = data.Length;
            string header = "";
            header = "HTTP/1.1 " + status + "\n";
            header += "Content-Type: " + mime + "\n";
            header += "Content-Lenght: " + dataLength.ToString() + "\n";
            header += "\n";
            header += data;
            writer.WriteLine(header);
        }

        public List<string> login(List<string> user)
        {
            dynamic jasondata = JObject.Parse(body);
            string name = jasondata.Username;
            string password = jasondata.Password;

            if (!db.passwordExists(name, password))
            {
                string mime = "text/plain";
                string status = "404 Not found";
                string data = "\nname oder password incorrect \n";
                Response(status, mime, data);
                return user;
            }
            else
            {
                user.Add(name + "-mtcgToken");
                string mime = "text/plain";
                string status = "200 Success";
                string data = "\nuser successful \n";
                Response(status, mime, data);
                return user;
            }
        }

        public void fromTypeToMethod(List<string> user)
        {
            switch (type)
            {
                case "GET":
                    handleGet(user);
                    break;
                case "PUT":
                    handlePut(user);
                    break;
                case "POST":
                    handlePost(user);
                    break;
                default: invalidType();
                    break;
            }
        }

        private void handlePost(List<string> user)
        {
            switch (order)
            {
                case "/packages":
                    addNewPackage(user);
                    break;
                case "/transaction/packages":
                    buyPackage(user);
                    break;
                case "/tradings":
                    //-----------------------------------------------------------------------------------???????
                    break;
                default:
                    invalidOrder();
                    break;
            }
        }


        private void handlePut(List<string> user)
        {
            switch (order)
            {
                case "/deck":
                    setDeck(user);
                    break;
                case "/deck/unset":
                    unsetDeck(user);
                    break;
                default:
                    invalidOrder();
                    break;
            }
        }
        private void handleGet(List<string> user)
        {
            switch (order)
            {
                case "/cards":
                    listCards(user);
                    break;
                case "/deck":
                    listDeck(user);
                    break;
                case "/stats":
                    listStats(user);
                    break;
                case "/score":
                    listScoreboard(user);
                    break;
                case "/tradings":
                    listTradings(user);
                    break;
                default:
                    invalidOrder();
                    break;
            }
        }
        public void invalidType()
        {
            string status = "404 Not Found";
            string mime = "text/plain";
            string data = "The type you're using is invalid";
            Response(status, mime, data);
        }

        public void invalidOrder()
        {
            string status = "404 Not Found";
            string mime = "text/plain";
            string data = "The order you're using is invalid";
            Response(status, mime, data);
        }

        public void listCards(List<string> user)
        {
            int lenght = authorization.IndexOf("-mtcgToken");
            string name = authorization.Substring(0, lenght);

            List<string> allCardsofPlayer = db.getAllCardsOfPlayer(name);
            string list = "";
            foreach (string card in allCardsofPlayer)
            {
                list += card + "\n\n";
            }
            string status = "200 Success";
            string mime = "text/plain";
            Response(status, mime, list);
            return;
        }

        public void listDeck(List<string> user)
        {
            int lenght = authorization.IndexOf("-mtcgToken");
            string name = authorization.Substring(0, lenght);

            List<string> allCardsofPlayer = db.getAllCardsInDeck(name);
            string list = "";
            foreach (string card in allCardsofPlayer)
            {
                list += card + "\n\n";
            }
            string status = "200 Success";
            string mime = "text/plain";
            Response(status, mime, list);
            return;
        }

        public void listStats(List<string> user)
        {
            int lenght = authorization.IndexOf("-mtcgToken");
            string name = authorization.Substring(0, lenght);
            string points = db.getPoints(name).ToString();
            if (points != "0")
            {
                string status = "200 Success";
                string mime = "text/plain";
                Response(status, mime, points);
            }
            else
            {
                string data = "\nUnexpected Database Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                Response(status, mime, data);
                return;
            }
        }

        public void listScoreboard(List<string> user)
        {
            string scoreboard = db.showScoreboard();
            if (scoreboard != "0")
            {
                string data = "\nUnexpected Database Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                Response(status, mime, data);
            }
            else
            {
                string status = "200 Success";
                string mime = "text/plain";
                Response(status, mime, scoreboard);
            }
        }

        public void listTradings(List<string> user)
        {
            //to be continued
        }

    }
}
