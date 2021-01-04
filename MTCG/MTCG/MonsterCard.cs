using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG
{
    public class MonsterCard : Card
    {
        public override ElementType ElementType { get; }
        public override CardType CardType { get; }
        public override MonsterType MonsterType { get; }
        public override float Damage { get; set; }

        public MonsterCard(ElementType _element, MonsterType _monsterType, float damage)
        {
            ElementType = _element;
            MonsterType = _monsterType;
            CardType = CardType.Monster;
            Damage = damage;
        }
        /*public override float Attack(Card Card)
        {
            return Damage;
        }*/

        public override float Attack(Card Card)
        {
            float Temp = Damage;
            if (Card.CardType == CardType.Spell)
            {
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
            }
            Damage = Temp;
            return Damage;
        }
        
    }
}
