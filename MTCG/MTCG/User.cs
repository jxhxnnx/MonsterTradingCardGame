using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public int Elo { get; set; }
        public int GameCounter { get; set; }
        public List<Card> Deck { get; set; } = new List<Card>(4);
        public List<Card> Stack { get; set; } = new List<Card>();

        public User(string _username, string _password)
        {
            Username = _username;
            Password = _password;
            Coins = 20;
            GameCounter = 0;
            Elo = 0;
        }




    }




}