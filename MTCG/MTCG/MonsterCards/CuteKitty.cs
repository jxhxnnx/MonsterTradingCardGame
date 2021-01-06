using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class CuteKitty : MonsterCard
    {
        public CuteKitty(double damage) : base(ElementType.Normal, MonsterType.CuteKitty, damage)
        {

        }

        public override double Attack(Card Card)
        {
            if (Card.ElementType == ElementType.Water)
            {
                Damage = Damage/2;
            }
            else
            {
                Damage = Damage * 10;
            }
            return Damage;
        }
    }
}
