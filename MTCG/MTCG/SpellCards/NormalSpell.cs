using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class NormalSpell : SpellCard
    {
        public NormalSpell(float damage) : base(ElementType.Normal, damage)
        {

        }
        public override float Attack(Card Card)
        {
            if (Card.MonsterType == MonsterType.Kraken)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
