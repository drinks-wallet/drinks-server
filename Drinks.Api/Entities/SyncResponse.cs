using System;
using System.Linq;
using Drinks.SipHasher;
using JetBrains.Annotations;

namespace Drinks.Api.Entities
{
    public class SyncResponse : TerminalResponseBase
    {
        public SyncResponse() { }

        public SyncResponse([NotNull] string header, [NotNull] string[] products, int time)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("The Header value must not be null or whitespace.");
            if (products == null)
                throw new ArgumentNullException("products");
            if (!products.Any())
                throw new ArgumentException("The Products array must not be empty.");
            if (time == default(uint))
                throw new ArgumentException("The Time value must be set.");

            Header = RemoveDiacritics(header);
            Products = products.Select(RemoveDiacritics).ToArray();
            Time = time;
            Hash = GenerateHash();
        }

        [UsedImplicitly]
        public string Header { get; set; }
        [UsedImplicitly]
        public string[] Products { get; set; }
        [UsedImplicitly]
        public int Time { get; set; }
        [UsedImplicitly]
        public string Hash { get; set; }

        string GenerateHash()
        {
            var productsString = string.Join(string.Empty, Products);
            var message = Header + productsString + Time;
            var hasher = new DrinksSipHasher();
            return hasher.CalculateHash(message, ConfigurationFacade.LocalHashKey);
        }
    }
}