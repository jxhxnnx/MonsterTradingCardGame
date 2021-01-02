using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class FireSpell : SpellCard
    {
        public FireSpell() : base(ElementType.Fire, 25)
        {

        }
        public override int Attack(Card Card)
        {
            if (Card.MonsterType == MonsterType.Kraken)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
