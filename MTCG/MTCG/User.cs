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
        public bool GameOver { get; set; }

        public User(string _username, string _password)
        {
            this.Username = _username;
            this.Password = _password;
            this.Coins = 20;
            this.GameOver = false;
            this.GameCounter = 0;
            this.Elo = 0;
        }
    }




}