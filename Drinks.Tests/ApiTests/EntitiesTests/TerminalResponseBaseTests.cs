using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drinks.Tests.ApiTests.EntitiesTests
{
    using Drinks.Api.Entities;

    [TestClass]
    public class TerminalResponseBaseTests
    {
        [TestMethod]
        public void SanitizeTextRemovesDiacritics()
        {
            const string diacriticizedText = "éàç";
            const string expectedText = "eac";
            var actualText = TerminalResponseBase.RemoveDiacritics(diacriticizedText);
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod]
        public void SanitizeTextLeavesNonDiacriticizedLettersIntact()
        {
            const string diacriticizedText = "Lévi";
            const string expectedText = "Levi";
            var actualText = TerminalResponseBase.RemoveDiacritics(diacriticizedText);
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod]
        public void SanitizeTextReturnsNullWhenNullPassed()
        {
            const string nullText = null;
            var actualText = TerminalResponseBase.RemoveDiacritics(nullText);
            Assert.AreEqual(nullText, actualText);
        }

        [TestMethod]
        public void SanitizeTextReturnsEmptyWhenEmptyPassed()
        {
            var emptyText = string.Empty;
            var actualText = TerminalResponseBase.RemoveDiacritics(emptyText);
            Assert.AreEqual(emptyText, actualText);
        }

        [TestMethod]
        public void SanitizeTextReturnsWhitespaceWhenWhitespacePassed()
        {
            const string whitespaceText = "   ";
            var actualText = TerminalResponseBase.RemoveDiacritics(whitespaceText);
            Assert.AreEqual(whitespaceText, actualText);
        }
    }
}