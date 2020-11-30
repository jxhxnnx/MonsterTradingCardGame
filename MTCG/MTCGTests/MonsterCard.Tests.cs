using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG.MTCG.Tests
{
    class MonsterCardTests
    {
        private MonsterCard _monsterCard;

        [SetUp]
        public void Setup()
        {
            _monsterCard = new MonsterCard(ElementType.Fire, 30, MonsterType.Dragon);
        }

        [Test]
        public void MonsterCard_Damage()
        {
            Assert.That(_monsterCard.Damage, Is.InRange(0, 100));
        }

        [Test]
        public void MonsterCard_NotNull()
        {
            Assert.IsNotNull(_monsterCard);
            Assert.That(_monsterCard, Is.Not.Null);
        }

        [Test]
        public void MonsterCard_ElementTypeCorrect()
        {
            Assert.AreEqual(ElementType.Fire, _monsterCard.ElementType);
        }

        [Test]
        public void MonsterCard_CardTypeCorrect()
        {
            Assert.AreEqual(CardType.Monster, _monsterCard.CardType);
        }

        [Test]
        public void MonsterCard_MonsterTypeCorrect()
        {
            Assert.AreEqual(MonsterType.Dragon, _monsterCard.MonsterType);
        }

        [Test]
        public void MonsterCard_SetDamageCorrect()
        {
            SpellCard Enemy = new SpellCard(ElementType.Normal, 30);
            _monsterCard.SetDamage(Enemy);
            Assert.AreEqual(_monsterCard.Damage, 60);
        }
    }
}
