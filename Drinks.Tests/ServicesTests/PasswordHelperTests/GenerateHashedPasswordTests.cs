namespace Drinks.Tests.ServicesTests.PasswordHelperTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Drinks.Services;

    [TestClass]
    public class GenerateHashedPasswordTests
    {
        [TestMethod]
        public void HashedPasswordIs64BytesLong()
        {
            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var hashedPassword = passwordHelper.GenerateHashedPassword("", out salt);

            Assert.AreEqual(64, hashedPassword.Length);
        }

        [TestMethod]
        public void SaltIs64BytesLong()
        {
            var passwordHelper = new PasswordHelper();
            byte[] salt;
            passwordHelper.GenerateHashedPassword("", out salt);

            Assert.AreEqual(64, salt.Length);
        }

        [TestMethod]
        public void SaltIsRandomlyGenerated()
        {
            const int saltCount = 10;
            var salts = new byte[saltCount][];

            var passwordHelper = new PasswordHelper();

            for (var i = 0; i < saltCount; i++)
            {
                passwordHelper.GenerateHashedPassword("", out salts[i]);
            }

            for (var i = 0; i < saltCount; i++)
            {
                for (var j = i + 1; j < saltCount; j++)
                {
                    Assert.IsFalse(salts[i].SequenceEqual(salts[j]));
                }
            }
        }

        [TestMethod]
        public void SamePasswordHashedTwiceGivesDifferentResults()
        {
            const string password = "mariannewiththeshakyhand";
            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var saltedPassword1 = passwordHelper.GenerateHashedPassword(password, out salt);
            var saltedPassword2 = passwordHelper.GenerateHashedPassword(password, out salt);
            Assert.AreNotEqual(saltedPassword1, saltedPassword2);
        }
    }
}
