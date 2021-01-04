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
        public List<string> GameLog = new List<string>();
        DB db = new DB();


        public Battle(User playerOne, User playerTwo)
        {
            PlayerOne = playerOne;
            DeckOne = deckBuilder(PlayerOne.Username);
            //PlayerOne.GameCounter++;
            PlayerTwo = playerTwo;
            DeckTwo = deckBuilder(PlayerTwo.Username);
            //PlayerTwo.GameCounter++;
            Rounds = 1;
          
        }

        public bool GameOver(List<Card> DeckOne, List<Card> DeckTwo, int Rounds)
        {
            if(DeckOne.Count == 0 || DeckTwo.Count == 0 || Rounds == 100)
            {
                return true;
            }
            return false;
        }

       public Card cardBuilder(string cardid)
       {
            string cardname = db.getCardName(cardid);
            float damage = db.getDamage(cardid);
            switch(cardname)
            {
                case "Dragon":
                    Card dragon = new MonsterCards.Dragon(damage);
                    return dragon;
                case "FireElve":
                    Card fireElve = new MonsterCards.FireElve(damage);
                    return fireElve;
                case "Goblin":
                    Card goblin = new MonsterCards.Goblin(damage);
                    return goblin;
                case "Knight":
                    Card knight = new MonsterCards.Knight(damage);
                    return knight;
                case "Kraken":
                    Card kraken = new MonsterCards.Kraken(damage);
                    return kraken;
                case "Ork":
                    Card ork = new MonsterCards.Ork(damage);
                    return ork;
                case "FireSpell":
                    Card fireSpell = new SpellCards.FireSpell(damage);
                    return fireSpell;
                case "NormalSpell":
                    Card normalSpell = new SpellCards.NormalSpell(damage);
                    return normalSpell;
                case "WaterSpell":
                    Card waterSpell = new SpellCards.WaterSpell(damage);
                    return waterSpell;
                default:
                    return null;
            }
       }
        public List<Card> deckBuilder(string playername)
        {
            List<string> cards = new List<string>();
            cards = db.getPlayerDeckCards(playername);

            List<Card> deck = new List<Card>();
            foreach(string x in cards)
            {
                deck.Add(cardBuilder(x));
            }
            return deck;
        }

        public void battleProcedure(List<Card> DeckOne, List<Card> DeckTwo)
        {
            int rounds = 0;
            Random rnd = new Random();
            int usedCardPlayerOne;  
            int usedCardPlayerTwo;
            float damagePlayerOne;  
            float damagePlayerTwo; 
            while (!GameOver(DeckOne,DeckTwo, rounds))
            {
                rounds++;
                usedCardPlayerOne = rnd.Next(1, DeckOne.Count);
                usedCardPlayerTwo = rnd.Next(1, DeckTwo.Count);
                damagePlayerOne = DeckOne[usedCardPlayerOne].Damage;
                damagePlayerTwo = DeckTwo[usedCardPlayerTwo].Damage;
                
                GameLog.Add("Card " + DeckOne[usedCardPlayerOne].MonsterType
                            + " vs. " + DeckOne[usedCardPlayerOne].MonsterType);
                damagePlayerOne = DeckOne[usedCardPlayerOne].Attack(DeckTwo[usedCardPlayerTwo]);
                damagePlayerTwo = DeckTwo[usedCardPlayerTwo].Attack(DeckOne[usedCardPlayerOne]);

                if(damagePlayerOne > damagePlayerTwo)
                {
                    DeckOne.Add(DeckTwo[usedCardPlayerTwo]);
                    DeckTwo.RemoveAt(usedCardPlayerTwo);
                    GameLog.Add(PlayerOne.Username + " wins round #" + rounds);
                } 
                else if (damagePlayerOne < damagePlayerTwo)
                {
                    DeckTwo.Add(DeckOne[usedCardPlayerOne]);
                    DeckOne.RemoveAt(usedCardPlayerOne);
                    GameLog.Add(PlayerTwo.Username + " wins round #" + rounds);
                } else
                {
                    GameLog.Add("No winner in round #" + rounds);
                }
            }
            if(DeckOne.Count == 0)
            {
                GameLog.Add(PlayerTwo.Username + " is the glorious victor.");
                db.updatePoints(PlayerOne.Username, - 5);
                db.updatePoints(PlayerTwo.Username, + 3);
            }
            else if(DeckTwo.Count == 0)
            {
                GameLog.Add(PlayerOne.Username + " is the glorious victor.");
                db.updatePoints(PlayerTwo.Username, - 5);
                db.updatePoints(PlayerOne.Username, + 3);
            }
            else
            {
                GameLog.Add("No winner in this battle. At least both of you get +1 Point for trying :)");
                db.updatePoints(PlayerOne.Username, + 1);
                db.updatePoints(PlayerTwo.Username, + 1);
            }
        }
    }
}
