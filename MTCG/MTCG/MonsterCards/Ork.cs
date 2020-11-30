using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Ork : MonsterCard
    {
        public Ork() : base(ElementType.Normal, 25, MonsterType.Ork)
        {

        }

        public override int Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Wizzard)
            {
                this.Damage = 0;
            }
            return this.Damage;
        }
    }
}
