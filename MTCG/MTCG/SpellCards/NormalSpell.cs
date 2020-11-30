using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.SpellCards
{
    public class NormalSpell : SpellCard
    {
        public NormalSpell() : base(ElementType.Normal, 25)
        {

        }
        public override int Attack(Card Card)
        {
            if (Card.MonsterType == MonsterType.Kraken)
            {
                this.Damage = 0;
            }
            return this.Damage;
        }
    }
}
