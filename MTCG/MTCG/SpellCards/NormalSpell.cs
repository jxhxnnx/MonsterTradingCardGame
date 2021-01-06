using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class NormalSpell : SpellCard
    {
        public NormalSpell(double damage) : base(ElementType.Normal, damage)
        {

        }
        public override double Attack(Card Card)
        {
            if (Card.MonsterType == MonsterType.Kraken)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
