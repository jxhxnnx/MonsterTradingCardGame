using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class FireSpell : SpellCard
    {
        public FireSpell(float damage) : base(ElementType.Fire, damage)
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
