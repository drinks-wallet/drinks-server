using System;
using Drinks.Entities.Exceptions;
using Drinks.SipHasher;
using Drinks.Entities.Extensions;

namespace Drinks.Entities
{
    public class BuyRequest
    {
        public string Badge { get; set; }
        public byte Product { get; set; }
        public int Time { get; set; }
        public string Hash { get; set; }

        public void Validate(string littleEndianKey)
        {
            ValidateHash(littleEndianKey);
            ValidateTimestamp();
        }

        void ValidateHash(string littleEndianKey)
        {
            var message = Badge + Product + Time;
            var hasher = new DrinksSipHasher();
            var hash = hasher.CalculateHash(message, littleEndianKey);
            
            if (!hash.Equals(Hash, StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidHashException();
        }

        // TODO: Reimplement this after testing.
        // TODO: Put this in the config.
        void ValidateTimestamp()
        {
            //if (Time.FromUnixTimestamp().Subtract(DateTime.Now).Duration() > TimeSpan.FromMinutes(5))
            //    throw new InvalidTimestampException();
        }
    }
}