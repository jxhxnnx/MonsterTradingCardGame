using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class WaterSpell : SpellCard
    {
        public WaterSpell(float damage) : base(ElementType.Water, damage)
        {

        }

        public override float Attack(Card Card)
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
