using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class WaterSpell : SpellCard
    {
        public WaterSpell() : base(ElementType.Water, 25)
        {

        }

        public override int Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Kraken)
            {
                Damage = 0;
            }
            if(Card.MonsterType == MonsterType.Knight)
            {
                Damage = 1000;
            }
            return Damage;
        }
    }
}
