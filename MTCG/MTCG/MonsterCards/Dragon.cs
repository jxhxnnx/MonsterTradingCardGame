using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG.MonsterCards
{
    public class Dragon : MonsterCard
    {
        public Dragon() : base (ElementType.Fire, 50, MonsterType.Dragon) 
        {

        }

        public override int Attack(Card Card)
        {
            if(Card.MonsterType == MonsterType.FireElve)
            {
                this.Damage = 0;
            }
            return this.Damage;
        }
    }
}
