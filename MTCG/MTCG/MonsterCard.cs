﻿using System;
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
            ElementType = _element;
            Damage = _damage;
            MonsterType = _monsterType;
            CardType = CardType.Monster;
        }
        public override int Attack(Card Card)
        {
            return Damage;
        }

        public override void SetDamage(Card Card)
        {
            int Temp = Damage;
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
        }
    }
}
