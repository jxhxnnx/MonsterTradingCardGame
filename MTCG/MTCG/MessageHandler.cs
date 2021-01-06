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
        public string command;
        public string authorization;
        public string body;
        public List<string> user;
        DB db;
        public List<string> loggedInUser;
        static object lockObj = new object();

        public MessageHandler(TcpClient _client, string _type, string _command, string _authorization, string _body, List<string> _user)
        {
            client = _client;
            type = _type;
            command = _command;
            authorization = _authorization;
            body = _body;
            db = new DB();
            user = _user;
            loggedInUser = new List<string>();

        }
        private string ExtractUsername(string authorization)
        {
            string[] username = authorization.Split("-");
            return username[0];
        }
        public void Response(string status, string mime, string data)
        {
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            StringBuilder mystring = new StringBuilder();
            mystring.AppendLine("HTTP/1.1 " + status);
            mystring.AppendLine("Content-Type: " + mime);
            mystring.AppendLine("Content-Length: " + data.Length.ToString());
            mystring.AppendLine();
            mystring.AppendLine(data);
            System.Diagnostics.Debug.WriteLine(mystring.ToString());
            writer.Write(mystring.ToString());
        }

        public List<string> login(List<string> user)
        {
            lock (lockObj)
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
                    string status = "200 OK";
                    string data = "\nuser OK \n";
                    Response(status, mime, data);
                    loggedInUser.Add(name);
                    return user;
                }
            }
        }

        public bool loggedIn(string name)
        {
            if (loggedInUser.Contains(name))
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public void fromTypeToMethod(List<string> user)
        {
            if (loggedIn(ExtractUsername(authorization)))
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
                    default:
                        invalidType();
                        break;
                }
            }
            else
            {
                string data = "\nLogIn required \n";
                string status = "404 Not found";
                string mime = "text/plain";
                Response(status, mime, data);
            }
        }

        public void handlePost(List<string> user)
        {
            switch (command)
            {
                //case "/battles":
                //battleHandler(user);
                //break;
                case "/packages":
                    addNewPackage(user);
                    break;
                case "/transactions/packages":
                    buyPackage(user);
                    break;
                case "/tradings":
                    //-----------------------------------------------------------------------------------???????
                    break;
                default:
                    invalidCommand();
                    break;
            }
        }


        private void handlePut(List<string> user)
        {
            switch (command)
            {
                case "/deck":
                    setDeck(user);
                    break;
                case "/deck/unset":
                    unsetDeck(user);
                    break;
                default:
                    invalidCommand();
                    break;
            }
        }
        private void handleGet(List<string> user)
        {
            switch (command)
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
                    invalidCommand();
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

        public void invalidCommand()
        {
            string status = "404 Not Found";
            string mime = "text/plain";
            string data = "The Command you're using is invalid";
            Response(status, mime, data);
        }

        public void listCards(List<string> user)
        {
            string name = ExtractUsername(authorization);

            List<string> allCardsofPlayer = db.getAllCardsOfPlayer(name);
            string list = "";
            foreach (string card in allCardsofPlayer)
            {
                list += card + "\n\n";
            }
            string status = "200 OK";
            string mime = "text/plain";
            Response(status, mime, list);
            return;
        }

        public void listDeck(List<string> user)
        {

            string name = ExtractUsername(authorization);

            List<string> allCardsofPlayer = db.getAllCardsInDeck(name);
            string list = "";
            foreach (string card in allCardsofPlayer)
            {
                list += card + "\n\n";
            }
            string status = "200 OK";
            string mime = "text/plain";
            Response(status, mime, list);
            return;
        }

        public void listStats(List<string> user)
        {
            string name = ExtractUsername(authorization);
            string points = db.getPoints(name).ToString();
            if (points != "0")
            {
                string status = "200 OK";
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
            if (scoreboard == "0")
            {
                string data = "\nUnexpected Database Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                Response(status, mime, data);
            }
            else
            {
                string status = "200 OK";
                string mime = "text/plain";
                Response(status, mime, scoreboard);
            }
        }

        public void listTradings(List<string> user)
        {
            //to be continued
        }

        public void registerUser()
        {
            dynamic jasondata = JObject.Parse(body);
            string name = jasondata.Username;
            string password = jasondata.Password;
            bool admin = false;
            if (db.addPlayer(name, password, admin))
            {
                string data = "\nPlayer successful created\n";
                string status = "200 OK";
                string mime = "text/plain";
                Response(status, mime, data);
            }
            else
            {
                string data = "\nUnexpected Database Error\n";
                string status = "200 OK";
                string mime = "text/plain";
                Response(status, mime, data);
            }
        }
        public void addNewPackage(List<string> user)
        {
            if (authorization != "admin-mtcgToken")
            {
                string status = "text/plain";
                string mime = "404 Not Found";
                string data = "\nonly admin is allowed to create packages";
                Response(status, mime, data);
                return;
            }
            else
            {
                JArray jasarray = JArray.Parse(body);
                var obj = jasarray[0];
                int packid = db.getMaxIDfromPackage() + 1;

                for (int i = 0; i < jasarray.Count; i++)
                {
                    string cardid = (string)jasarray[i]["Id"];
                    string cardname = (string)jasarray[i]["Name"];
                    double damage = (double)jasarray[i]["Damage"];
                    //string type = (string)jasarray[i]["Cardtype"];
                    //string element = (string)jasarray[i]["Element"];
                    db.addCard(cardid, cardname, damage, "-", "-");
                    db.addPackage(cardid, packid, false);
                }

                string data = "\nPackage created OKfully\n";
                string status = "200 OK";
                string mime = "text/plain";

                Response(status, mime, data);
            }
        }

        public void buyPackage(List<string> user)
        {
            string status = "";
            string mime = "";
            string data = "";
            string name = ExtractUsername(authorization);
            if (db.getCoins(name) < 5)
            {
                data = "\nSorry, you need at least 5 coins to buy a package\n";
                status = "200 OK";
                mime = "text/plain";
                Response(status, mime, data);
                return;
            }
            else if (!db.getAvailablePackages())
            {
                data = "\nSorry, no package left\n";
                status = "200 OK";
                mime = "text/plain";
                Response(status, mime, data);
                return;
            }
            else
            {
                int packid = db.getIDfromPackage();
                foreach (var cardid in db.getCardsOfPackage(packid))
                {
                    db.buyPackage(name, cardid);
                    db.sellPackage(cardid, true);
                }
                db.updateCoins(name, 5);

                data = "\nPackage aquired OKfully\n";
                status = "200 OK";
                mime = "text/plain";
                Response(status, mime, data);
            }
        }

        public void setDeck(List<string> user)
        {
            string status = "";
            string mime = "";
            string data = "";
            string name = ExtractUsername(authorization);
            JArray jasarray = JArray.Parse(body);
            if (jasarray.Count < 4)
            {
                data = "\nnot enough cards: you need 4 cards in your deck\n";
                status = "404 Not found";
                mime = "text/plain";
                Response(status, mime, data);
                return;
            }
            else if (jasarray.Count > 4)
            {
                data = "\ntoo many cards: you need 4 cards in your deck\n";
                status = "404 Not found";
                mime = "text/plain";
                Response(status, mime, data);
                return;
            }
            else if (db.existCardsInDeck(name) != 0)
            {
                data = "\nDeck is already set\n";
                status = "404 Not found";
                mime = "text/plain";
                Response(status, mime, data);
                return;
            }
            else if (jasarray.Count == 4 && db.existCardsInDeck(name) == 0)
            {
                foreach (string cardid in jasarray)
                {
                    db.defineDeck(name, cardid, true);
                }
                data = "\nDeck is ready to rumble\n";
                status = "200 OK";
                mime = "text/plain";
                Response(status, mime, data);
            }
        }

        public void unsetDeck(List<string> user)
        {
            string status = "";
            string mime = "";
            string data = "";
            string name = ExtractUsername(authorization);

            db.unsetDeck(name);
            data = "\nDeck is not ready to rumble anymore\n";
            status = "200 OK";
            mime = "text/plain";
            Response(status, mime, data);
        }
    }
}
