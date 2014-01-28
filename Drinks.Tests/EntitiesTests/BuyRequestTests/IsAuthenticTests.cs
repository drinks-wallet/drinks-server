using System;
using Drinks.Entities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drinks.Tests.EntitiesTests.BuyRequestTests
{
    using Drinks.Entities;

    [TestClass]
    public class IsAuthenticTests
    {
        const string Key = "0F0E0D0C0B0A09080706050403020100";

        readonly BuyRequest authenticRequest = new BuyRequest
        {
            Badge = "0107CB5A72",
            Product = 0,
            Time = DateTime.Now.ToUnixTimestamp(),
            Hash = "5B4C1820ABE95E23"
        };

        [TestMethod]
        public void AuthenticRequestIsAuthentic()
        {
            authenticRequest.Validate(Key);
        }
    }
}
