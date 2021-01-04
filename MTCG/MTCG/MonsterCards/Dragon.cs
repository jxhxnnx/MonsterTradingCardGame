using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    
    public class Dragon : MonsterCard
    {
        public Dragon(float damage) : base (ElementType.Fire, MonsterType.Dragon, damage) 
        {

        }

        public override float Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.FireElve)
            {
                Damage = 0;
            }
            return Damage;
        }
    }
}
