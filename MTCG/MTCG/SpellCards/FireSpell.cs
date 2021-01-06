using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class FireSpell : SpellCard
    {
        public FireSpell(double damage) : base(ElementType.Fire, damage)
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
