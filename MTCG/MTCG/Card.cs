using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public abstract class Card
    {
        public abstract int Damage { get; set; }
        public abstract ElementType ElementType { get; }
        public abstract CardType CardType { get;  }
        public abstract MonsterType MonsterType { get; }
        public abstract int Attack(Card Card);
        public abstract void SetDamage(Card Card);     
    }
}