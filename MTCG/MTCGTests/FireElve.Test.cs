using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCGTests
{
    [TestFixture]
    public class FireElveTest
    {
        public MTCG.MonsterCards.FireElve _FireElve;

        [SetUp]
        public void Setup()
        {
            _FireElve = new MTCG.MonsterCards.FireElve();
        }

        [Test]
        public void FireElve_IsNotNull()
        {
            Assert.IsNotNull(_FireElve);
            Assert.That(_FireElve, Is.Not.Null);
        }

        [Test]
        public void SpellCard_DamageIsInRange()
        {
            Assert.AreEqual(_FireElve.Damage, 30);
        }

        [Test]
        public void FireElve_IsElementTypeCorrect()
        {
            Assert.AreEqual(MTCG.ElementType.Fire, _FireElve.ElementType);
        }

        [Test]
        public void FireElve_IsCardTypeCorrect()
        {
            Assert.AreEqual(MTCG.CardType.Monster, _FireElve.CardType);
        }

        [Test]
        public void FireElve_IsMonsterTypeCorrect()
        {
            Assert.AreEqual(MTCG.MonsterType.FireElve, _FireElve.MonsterType);
        }
    }
}
