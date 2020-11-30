using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public enum CardType
    {
        Spell, Monster
    }
    public enum ElementType
    {
        Fire, Water, Normal
    }
    public enum MonsterType
    {
        Typeless,
        Goblin,
        Dragon,
        Wizzard,
        Ork,
        Knight,
        Kraken,
        FireElve
    }
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