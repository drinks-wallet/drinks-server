using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drinks.Tests.ServicesTests.PasswordHelperTests
{
    using Drinks.Services;

    [TestClass]
    public class ValidatePasswordTests
    {
        [TestMethod]
        public void ValidPasswordValidates()
        {
            const string validPassword = "meatybeatybigandbouncy";

            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var hashedPassword = passwordHelper.GenerateHashedPassword(validPassword, out salt);

            Assert.IsTrue(passwordHelper.ValidatePassword(validPassword, salt, hashedPassword));
        }

        [TestMethod]
        public void InvalidPasswordWithCorrectSaltDoesNotValidate()
        {
            const string validPassword = "mariannewiththeshakyhand";
            const string invalidPassword = "Mariannewiththeshakyhand";

            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var hashedPassword = passwordHelper.GenerateHashedPassword(validPassword, out salt);

            Assert.IsFalse(passwordHelper.ValidatePassword(invalidPassword, salt, hashedPassword));
        }

        [TestMethod]
        public void ValidPasswordWithIncorrectSaltDoesNotValidate()
        {
            const string validPassword = "mariannewiththeshakyhand";

            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var hashedPassword = passwordHelper.GenerateHashedPassword(validPassword, out salt);
            salt = passwordHelper.GenerateSalt();

            Assert.IsFalse(passwordHelper.ValidatePassword(validPassword, salt, hashedPassword));
        }

        [TestMethod]
        public void InvalidPasswordWithIncorrectSaltDoesNotValidate()
        {
            const string validPassword = "mariannewiththeshakyhand";
            const string invalidPassword = "Mariannewiththeshakyhand";

            var passwordHelper = new PasswordHelper();
            byte[] salt;
            var hashedPassword = passwordHelper.GenerateHashedPassword(validPassword, out salt);
            salt = passwordHelper.GenerateSalt();

            Assert.IsFalse(passwordHelper.ValidatePassword(invalidPassword, salt, hashedPassword));
        }
    }
}
