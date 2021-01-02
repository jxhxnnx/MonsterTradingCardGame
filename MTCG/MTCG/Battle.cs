using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class Battle
    {
        public User PlayerOne;
        public User PlayerTwo;
        public int Rounds;
        public User Winner;
        public List<Card> DeckOne = new List<Card>();
        public List<Card> DeckTwo = new List<Card>();


        public Battle(User playerOne, User playerTwo)
        {
            PlayerOne = playerOne;
            DeckOne = PlayerOne.Deck;
            PlayerOne.GameCounter++;
            PlayerTwo = playerTwo;
            DeckTwo = PlayerTwo.Deck;
            PlayerTwo.GameCounter++;
            Rounds = 1;
          
        }

        public bool GameOver()
        {
            if(DeckOne.Count == 0 || DeckTwo.Count == 0)
            {
                return true;
            }
            return false;
        }

    }
}
