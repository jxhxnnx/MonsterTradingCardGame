using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public abstract class Card
    {
        public abstract float Damage { get; set; }
        public abstract ElementType ElementType { get; }
        public abstract CardType CardType { get;  }
        public abstract MonsterType MonsterType { get; }
        public abstract float Attack(Card Card);
        public string CardId { get; set; }
        //public abstract float SetDamage(Card Card, string cardid);
        public DB db = new DB();
    }
}