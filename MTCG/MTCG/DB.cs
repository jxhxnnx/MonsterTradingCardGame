using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace MTCG
{
    public class DB // IDB
    {
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=passwort;Database=MTCG;");
        }

        // --------------------------------------------Player Name Functions----------------------------
        public bool addPlayer(string name, string password, bool admin)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            if(!playerExists(name))
            {
                var query = "INSERT INTO player(name, password, coins, points, admin) VALUES(@name, @password, 20, 100, @admin)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("admin", admin);
                cmd.Prepare();
                int n = cmd.ExecuteNonQuery();
                con.Close();
                if (n == 1)
                {
                    Console.WriteLine("New player inserted");
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                Console.WriteLine("Player already exists.");
                return false;
            }
            
            
        }

        public bool changePlayerName(string name, string newName)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE player SET name = @newName WHERE name = @name";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);

            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("newName", newName);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("Name changed to " + newName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool playerExists(string name) //check if player exists
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM player WHERE name = @name";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            if (count != 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        public bool passwordExists(string name, string password) //check if password and player exist and match
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM player WHERE name = @name AND password = @password";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("password", password);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            if (count != 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

// --------------------------------------------Coins Functions----------------------------
        public int getCoins(string name)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT coins FROM player WHERE name = @name";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int coins = reader.GetInt32(0);
            con.Close();
            return coins;
        }

        public bool updateCoins(string name, int minusCoins)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE player SET coins = coins - @minusCoins WHERE name = @name";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("minusCoins", minusCoins);
            int n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                Console.WriteLine("Coins updated");
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

// -------------------------------------------Points Functions----------------------------
        public int getPoints(string name)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT points FROM player WHERE name = @name";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int points = reader.GetInt32(0);
            con.Close();
            return points;
        }

        public bool updatePoints(string name, int plusPoints)
        {
            NpgsqlConnection con = GetConnection();

            var query = "UPDATE player SET points = points + @plusPoints WHERE name = @player";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("plusPoints", plusPoints);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                Console.WriteLine("Points updated");
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
// -------------------------------------------Cards Functions----------------------------
        public void addCard(string cardid, string cardname, double damage, string type, string element)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            if(!cardExists(cardid))
            {
                var query = "INSERT INTO cards(cardid, cardname, damage, type, element) VALUES(@cardid, @cardname, @damage, @type, @element)";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Parameters.AddWithValue("cardname", cardname);
                cmd.Parameters.AddWithValue("damage", damage);
                cmd.Parameters.AddWithValue("type", type);
                cmd.Parameters.AddWithValue("element", element);
                cmd.Prepare();
                int n = cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                Console.WriteLine("Card already exists");
            }
        }

        public bool noCardsExists()
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM cards";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            if (count != 0)
            {
                con.Close();
                return false;
            }
            else
            {
                con.Close();
                return true;
            }
        }
        public bool cardExists(string cardid) //check if card exists
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM cards WHERE cardid = @cardid";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            if (count != 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        // -------------------------------------------Player_Cards Functions----------------------------
        public bool insert_player_card(string cardid, string name, bool deck)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "INSERT INTO cards(cardid, name, deck) VALUES(@cardid, @name, @deck)";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("deck", deck);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                Console.WriteLine("card to player inserted");
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        public bool defineDeck(string cardid, bool deck)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE player_card SET deck = @deck WHERE cardid = @cardid";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("deck", deck);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("deck updated");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool unsetDeck(string name)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE player_card SET deck = false WHERE playername = @name";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("deck is empty");
                return true;
            }
            else
            {
                return false;
            }
        }

        public int amountCardsInDeck(string name)
        {
            NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM player_card WHERE playername = @name and deck = true";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int amount = reader.GetInt32(0);
            con.Close();
            return amount;
        }

        public bool changeCardOwner(string cardid, string name)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE player_card SET playername = @name WHERE cardid = @cardid";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("playername updated");
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> getAllCardsOfPlayer(string playername)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT cardname, damage, type, element, deck FROM cards " +
                "INNER JOIN player_card ON cards.cardid = player_card.cardid WHERE playername = @playername";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("playername", playername);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            int count = 1;
            List<string> cardList = new List<string>();

            while (reader.Read())
            {
                string oneline = count.ToString() + ". Name: " + reader.GetString(0) + " /Damage: " +
                    reader.GetInt32(1).ToString() + " /Type: " + reader.GetString(2) + " /Element: " +
                    reader.GetString(3) + " /In deck?: " + reader.GetBoolean(4).ToString();
                cardList.Add(oneline);
                count++;
            }
            con.Close();
            return cardList;
        }

        public List<string> getAllCardsInDeck(string playername)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT cardname, damage, type, element, deck FROM cards " +
                "INNER JOIN player_card ON cards.cardid = player_card.cardid WHERE playername = @playername AND deck = true";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("playername", playername);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            int count = 1;
            List<string> cardList = new List<string>();

            while (reader.Read())
            {
                string oneline = count.ToString() + ". Name: " + reader.GetString(0) + " /Damage: " +
                    reader.GetInt32(1).ToString() + " /Type: " + reader.GetString(2) + " /Element: " +
                    reader.GetString(3) + " /In deck?: " + reader.GetBoolean(4).ToString();
                cardList.Add(oneline);
                count++;
            }
            con.Close();
            return cardList;
        }

//----------------------------------------------Package Functions-------------------------

        public bool getAvailablePackages()
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT COUNT(*) FROM package WHERE sold = false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
            if (query != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool sellPackage(string cardid, bool sold)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "UPDATE package SET sold = @sold WHERE cardid = @cardid";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("sold", sold);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("sold");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool buyPackage(string name, string cardid)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "INSERT INTO player_card(playername, cardid, deck) VALUES (@playername, @cardid, false)";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("playername", name);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n == 1)
            {
                Console.WriteLine("Shopping queen");
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> getCardsOfPackage(int packid)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT cardid FROM package WHERE packid = @packid";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("packid", packid);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            List<string> cardList = new List<string>();
            while (reader.Read())
            {
                string oneline = reader.GetString(0);
                cardList.Add(oneline);
            }
            con.Close();
            return cardList;
        }

        public int getIDfromPackage()
        {
            int id = 0;
            if(getAvailablePackages())
            {
                using NpgsqlConnection con = GetConnection();
                con.Open();
                var query = "SELECT min(packid) FROM package WHERE sold = false";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                con.Close();
            }
            
            return id;
        }

        public int getMaxIDfromPackage()
        {
            int id = 0;
            if (getAvailablePackages())
            {
                if(noCardsExists())
                {
                    id = 0;
                }
                else
                {
                    using NpgsqlConnection con = GetConnection();
                    con.Open();
                    var query = "SELECT max(packid) FROM package WHERE sold = false";
                    using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    using NpgsqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    id = reader.GetInt32(0);       
                    con.Close();
                }

            }
            return id;
        }

        public void addPackage(string cardid, int packid, bool sold)
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "INSERT INTO package(cardid, packid, sold) VALUES(@cardid, @packid, @sold)";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("packid", packid);
            cmd.Parameters.AddWithValue("sold", sold);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            con.Close();
        }
    
        public string showScoreboard()
        {
            using NpgsqlConnection con = GetConnection();
            con.Open();
            var query = "SELECT name, points FROM player ORDER BY points desc";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            int x = 1;
            string scoreboard = "********SCOREBOARD********\n";

            while (reader.Read())
            {
                scoreboard += "\n" + x.ToString() + ". Place: " + reader.GetString(0) + " /Points: " + reader.GetInt32(1).ToString();
                x++;
            }
            con.Close();
            return scoreboard;
        }
        
    }
}

