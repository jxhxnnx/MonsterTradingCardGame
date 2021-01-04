using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Goblin : MonsterCard
    {
        public Goblin(float damage) : base(ElementType.Normal, MonsterType.Goblin, damage)
        {

        }

        public override float Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Dragon)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
