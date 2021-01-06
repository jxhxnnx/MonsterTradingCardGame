using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Goblin : MonsterCard
    {
        public Goblin(double damage) : base(ElementType.Normal, MonsterType.Goblin, damage)
        {

        }

        public override double Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Dragon)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
