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
        public override int Damage { get; set; }

        public SpellCard(ElementType _element, int _damage)
        {
            this.ElementType = _element;
            this.Damage = _damage;
            this.MonsterType = MonsterType.Typeless;
            this.CardType = CardType.Spell;
        }
        public override int Attack(Card card)
        {
            return Damage;
        }

        public override void SetDamage(Card Card)
        {
            int Temp = this.Damage;
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
            
            this.Damage = Temp;
        }
    }
}