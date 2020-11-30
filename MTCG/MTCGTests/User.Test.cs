using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MTCG.MTCG.Tests
{
    class UserTest
    {
        private User _user;

        [SetUp]
        public void Setup()
        {
            _user = new User("TestUser", "TestPassword");
        }

        [Test]
        public void User_IsNotNull()
        {
            Assert.IsNotNull(_user);
            Assert.That(_user, Is.Not.Null);
        }

        [Test]
        public void User_NameIsEqual()
        {
            Assert.AreEqual(_user.Username, "TestUser");
        }

        [Test]
        public void User_PasswordIsEqual()
        {
            Assert.AreEqual(_user.Password, "TestPassword");
        }

        [Test]
        public void User_AmountOfCounts()
        {
            Assert.AreEqual(_user.Coins, 20);
        }

        [Test]
        public void User_GameOverIsFalse()
        {
            Assert.IsFalse(_user.GameOver);
        }

        [Test]
        public void User_AmountGames()
        {
            Assert.AreEqual(_user.GameCounter, 0);
        }

    }
}
