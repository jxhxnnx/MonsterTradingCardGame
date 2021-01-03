using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace MTCGTests
{
    [TestFixture]
    class DBTests
    {
        MTCG.DB db = new MTCG.DB();

        [Test]
        public void addingPlayerTrue()
        {
            bool newPlayer = db.addPlayer("admin", "istrator", false);
            Assert.IsFalse(newPlayer);
        }
        [Test]
        public void passwordExistsTrue()
        {
            bool checkpassword = db.passwordExists("admin", "istrator");
            bool checkFalsePassword = db.passwordExists("admin", "lulu");
            Assert.IsTrue(checkpassword);
            Assert.IsFalse(checkFalsePassword);
        }


    }
}
