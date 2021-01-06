using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class SpellCard : Card
    {
        public override ElementType ElementType { get; }
        public override CardType CardType { get; }
        public override MonsterType MonsterType { get; }
        public override double Damage { get; set; }

        public SpellCard(ElementType _element, double damage)
        {
            ElementType = _element;
            MonsterType = MonsterType.Typeless;
            CardType = CardType.Spell;
            Damage = damage;
        }
       /* public override double Attack(Card card)
        {
            return Damage;
        }*/

        public override double Attack(Card Card)
        {
            double Temp = Damage;
                if (ElementType == ElementType.Fire && Card.ElementType == ElementType.Water
                || ElementType == ElementType.Water && Card.ElementType == ElementType.Normal
                || ElementType == ElementType.Normal && Card.ElementType == ElementType.Fire)
                {
                    Temp = Temp / 2;
                }
                else if (ElementType == ElementType.Fire && Card.ElementType == ElementType.Normal
              || ElementType == ElementType.Water && Card.ElementType == ElementType.Fire
              || ElementType == ElementType.Normal && Card.ElementType == ElementType.Water)
                {
                    Temp = Temp * 2;
                }
            
            Damage = Temp;
            return Damage;
        }
    }
}