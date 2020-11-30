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
        public override int Damage { get; set; }

        public MonsterCard(ElementType _element, int _damage, MonsterType _monsterType)
        {
            this.ElementType = _element;
            this.Damage = _damage;
            this.MonsterType = _monsterType;
            this.CardType = CardType.Monster;
        }
        public override int Attack(Card Card)
        {
            return Damage;
        }

        public override void SetDamage(Card Card)
        {
            int Temp = this.Damage;
            if (Card.CardType == CardType.Spell)
            {
                if (this.ElementType == ElementType.Fire && Card.ElementType == ElementType.Water
                 || this.ElementType == ElementType.Water && Card.ElementType == ElementType.Normal
                 || this.ElementType == ElementType.Normal && Card.ElementType == ElementType.Fire)
                {
                    Temp = Temp / 2;
                }
                else if (this.ElementType == ElementType.Fire && Card.ElementType == ElementType.Normal
                      || this.ElementType == ElementType.Water && Card.ElementType == ElementType.Fire
                      || this.ElementType == ElementType.Normal && Card.ElementType == ElementType.Water)
                {
                    Temp = Temp * 2;
                }
            }
            this.Damage = Temp;
        }
    }
}
