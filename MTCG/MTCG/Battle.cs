using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class Battle
    {
        public int Rounds;
        public User Winner;
        public List<Card> DeckOne = new List<Card>();
        public List<Card> DeckTwo = new List<Card>();
        public List<string> GameLog;
        DB db = new DB();
        static object lockYou = new object();


        public Battle(User playerOne, User playerTwo)
        {

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
            double damage = db.getDamage(cardid);
            switch(cardname)
            {
                case "Dragon":
                    Card dragon = new MonsterCards.Dragon(damage);
                    return dragon;
                case "FireElf":
                    Card fireElve = new MonsterCards.FireElve(damage);
                    return fireElve;
                case "WaterGoblin":
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
                case "RegularSpell":
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

        public List<string> battleProcedure(User playerOne, User playerTwo)
        {
            lock(lockYou)
            {
                DeckOne = deckBuilder(playerOne.Username);
                DeckTwo = deckBuilder(playerTwo.Username);
                Rounds = 0;
                GameLog = new List<string>();

                Random rnd = new Random();
                int usedCardPlayerOne;
                int usedCardPlayerTwo;
                double damagePlayerOne;
                double damagePlayerTwo;
                while (!GameOver(DeckOne, DeckTwo, Rounds))
                {
                    Rounds++;
                    usedCardPlayerOne = rnd.Next(0, DeckOne.Count);
                    usedCardPlayerTwo = rnd.Next(0, DeckTwo.Count);
                    damagePlayerOne = DeckOne[usedCardPlayerOne].Damage;
                    damagePlayerTwo = DeckTwo[usedCardPlayerTwo].Damage;

                    GameLog.Add("Card " + DeckOne[usedCardPlayerOne].MonsterType
                                + " vs. " + DeckTwo[usedCardPlayerTwo].MonsterType + "\n");
                    damagePlayerOne = DeckOne[usedCardPlayerOne].Attack(DeckTwo[usedCardPlayerTwo]);
                    damagePlayerTwo = DeckTwo[usedCardPlayerTwo].Attack(DeckOne[usedCardPlayerOne]);

                    if (damagePlayerOne > damagePlayerTwo)
                    {
                        DeckOne.Add(DeckTwo[usedCardPlayerTwo]);
                        DeckTwo.RemoveAt(usedCardPlayerTwo);
                        GameLog.Add(playerOne.Username + " wins round #" + Rounds + "\n\n");
                    }
                    else if (damagePlayerOne < damagePlayerTwo)
                    {
                        DeckTwo.Add(DeckOne[usedCardPlayerOne]);
                        DeckOne.RemoveAt(usedCardPlayerOne);
                        GameLog.Add(playerTwo.Username + " wins round #" + Rounds + "\n\n");
                    }
                    else
                    {
                        GameLog.Add("No winner in round #" + Rounds + "\n\n");
                    }
                }
                if (DeckOne.Count == 0)
                {
                    GameLog.Add(playerTwo.Username + " is the glorious victor." + "\n\n");
                    db.updatePoints(playerOne.Username, -5);
                    db.updatePoints(playerTwo.Username, +3);
                }
                else if (DeckTwo.Count == 0)
                {
                    GameLog.Add(playerOne.Username + " is the glorious victor." + "\n\n");
                    db.updatePoints(playerTwo.Username, -5);
                    db.updatePoints(playerOne.Username, +3);
                }
                else
                {
                    GameLog.Add("No winner in this battle. At least both of you get +1 Point for trying :)" + "\n\n");
                    db.updatePoints(playerOne.Username, +1);
                    db.updatePoints(playerTwo.Username, +1);
                }
                return GameLog;
            }
        }
    }
}
