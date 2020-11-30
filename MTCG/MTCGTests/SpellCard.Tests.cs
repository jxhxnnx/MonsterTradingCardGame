using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG.MTCG.Tests
{
    class SpellCardTests
    {
        private SpellCard _spellCard;

        [SetUp]
        public void Setup()
        {
            _spellCard = new SpellCard(ElementType.Normal, 20);
        }

        [Test]
        public void SpellCard_DamageIsInRange()
        {
            Assert.That(_spellCard.Damage, Is.InRange(0, 100));
        }

        [Test]
        public void SpellCard_IsNotNull()
        {
            Assert.IsNotNull(_spellCard);
            Assert.That(_spellCard, Is.Not.Null);
        }

        [Test]
        public void SpellCard_IsElementTypeCorrect()
        {
            Assert.AreEqual(ElementType.Normal, _spellCard.ElementType);
        }

        [Test]
        public void SpellCard_IsCardTypeCorrect()
        {
            Assert.AreEqual(CardType.Spell, _spellCard.CardType);
        }

        [Test]
        public void SpellCard_IsMonsterTypeCorrect()
        {
            Assert.AreEqual(MonsterType.Typeless, _spellCard.MonsterType);
        }


    }
}
