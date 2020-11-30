using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using NUnit.Framework;

namespace MTCGTests
{
    [TestFixture]
    public class DragonTest
    {
        MTCG.MonsterCards.Dragon _dragon = new MTCG.MonsterCards.Dragon();


        [Test]
        public void Dragon_NotNull()
        {
            Assert.IsNotNull(_dragon);
            Assert.That(_dragon, Is.Not.Null);
        }

        [Test]
        public void Dragon_DamageAreEqual()
        {
            Assert.AreEqual(_dragon.Damage, 50);
        }

        [Test]
        public void Dragon_ElementTypeCorrect()
        {
            Assert.AreEqual(MTCG.ElementType.Fire, _dragon.ElementType);
        }

        [Test]
        public void Dragon_CardTypeCorrect()
        {
            Assert.AreEqual(MTCG.CardType.Monster, _dragon.CardType);
        }

        [Test]
        public void Dragon_MonsterTypeCorrect()
        {
            Assert.AreEqual(MTCG.MonsterType.Dragon, _dragon.MonsterType);
        }

        [Test]
        public void Dragon_DamageWithFireElvesZero()
        {
            MTCG.MonsterCards.FireElve Enemy = new MTCG.MonsterCards.FireElve();
            int ExpectedDamage = _dragon.Attack(Enemy);
            Assert.AreEqual(ExpectedDamage, 0);
        }
    }
}
