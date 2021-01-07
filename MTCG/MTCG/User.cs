using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class User
    {
        public string Username { get; set; }
        public int Coins { get; set; }
        public int Elo { get; set; }
        //public int GameCounter { get; set; }
        public List<Card> Deck { get; set; } = new List<Card>(4);
        public List<Card> Stack { get; set; } = new List<Card>();
        DB db = new DB();

        public User(string _username)
        {
            Username = _username;
            Coins = db.getCoins(_username);
            Elo = db.getPoints(_username);
        }




    }




}