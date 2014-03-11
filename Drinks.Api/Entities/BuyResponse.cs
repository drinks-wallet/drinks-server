using System;
using System.Collections.Generic;
using Drinks.Entities.Extensions;
using Drinks.SipHasher;
using JetBrains.Annotations;

namespace Drinks.Api.Entities
{
    public class BuyResponse : TerminalResponseBase
    {
        static readonly IReadOnlyDictionary<int, string> Melodies = new Dictionary<int, string>
        {
            { -1, "a1b1c1d1e1f1g1" }, { 0, "a1b1c1d1e1f1g1" }, { 1, "a1c1a1c1a1c1a1c1" }, { 2, "g1f1e1d1c1b1a1" }
        };

        [UsedImplicitly]
        public readonly string Melody;
        [UsedImplicitly]
        public readonly string[] Message;
        [UsedImplicitly]
        public readonly int Time;
        [UsedImplicitly]
        public readonly string Hash;

        /// <summary>
        /// Instantiates an instance of a "Valid" BuyResponse.
        /// </summary>
        public BuyResponse(string name, string balance, int productId)
        {
            Melody = GetMelody(productId);
            Message = new[] { RemoveDiacritics(name), balance };
            Time = DateTime.Now.ToUnixTimestamp();
            Hash = GenerateHash();
        }

        /// <summary>
        /// Instantiates an instance of an "Invalid Badge" BuyResponse
        /// </summary>
        public BuyResponse(string badgeId)
        {
            Melody = "c5";
            Message = new[] { "Invalid Badge", badgeId };
            Time = DateTime.Now.ToUnixTimestamp();
            Hash = GenerateHash();
        }
        
        public BuyResponse(BuyResponseStatus status)
        {
            switch (status)
            {
                case BuyResponseStatus.InsufficientFunds:
                    Melody = "c5";
                    Message = new[] { "Insufficient", "Funds" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.InvalidHash:
                    Melody = "c5";
                    Message = new[] { "Invalid", "Hash" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.InvalidProduct:
                    Melody = "c5";
                    Message = new[] { "Invalid", "Product" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.InvalidTimestamp:
                    Melody = "c5";
                    Message = new[] { "Invalid", "Time" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.DeserializationException:
                    Melody = "c5";
                    Message = new[] { "Deserialization", "Exception" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.Free:
                    Melody = "C4a2g2C3S3C4a2g2C3S3C4a2g2C3S3C4a2g2C3";
                    Message = new[] { "!!! You win !!!", "!!! Party time !!!" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status", "Valid and Invalid Badge responses must be generated with the appropriate constructor.");
            }
        }

        static string GetMelody(int productId)
        {
            return (!Melodies.ContainsKey(productId)) ? Melodies[-1] : Melodies[productId];
        }

        string GenerateHash()
        {
            var messageString = string.Join(string.Empty, Message);
            var message = Melody + messageString + Time;
            var hasher = new DrinksSipHasher();
            return hasher.CalculateHash(message, ConfigurationFacade.LocalHashKey);
        }
    }
}