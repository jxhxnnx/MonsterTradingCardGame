using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Ork : MonsterCard
    {
        public Ork(double damage) : base(ElementType.Normal, MonsterType.Ork, damage)
        {

        }

        public override double Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.Wizzard)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
