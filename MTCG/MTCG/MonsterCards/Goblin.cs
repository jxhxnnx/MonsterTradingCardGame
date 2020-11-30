using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Goblin : MonsterCard
    {
        public Goblin() : base(ElementType.Normal, 35, MonsterType.Goblin)
        {

        }

        public override int Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Dragon)
            {
                this.Damage = 0;
            }
            return this.Damage;
        }
    }
}
