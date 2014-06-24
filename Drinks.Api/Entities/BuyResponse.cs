using System;
using System.Collections.Generic;
using Drinks.Entities.Extensions;
using Drinks.SipHasher;
using JetBrains.Annotations;

namespace Drinks.Api.Entities
{
    public class BuyResponse : TerminalResponseBase
    {
        const string ErrorMelody = "c5";
        const string FreeMelody = "C4a2g2C3S3C4a2g2C3S3C4a2g2C3S3C4a2g2C3";
        
        [UsedImplicitly]
        public readonly string Melody;
        [UsedImplicitly]
        public readonly string[] Message;
        [UsedImplicitly]
        public readonly int Time;
        [UsedImplicitly]
        public readonly string Hash;

        static readonly IReadOnlyDictionary<int, string> ProductMelodies = new Dictionary<int, string>
        {
            { -1, "a1b1c1d1e1f1g1" }, { 0, "a1b1c1d1e1f1g1" }, { 1, "a1c1a1c1a1c1a1c1" }, { 2, "e2g2E2C2D2G2" }
        };
        
        readonly string[] InsufficientFundsMessage = { "Insufficient", "Funds" };
        readonly string[] InvalidHashMessage = { "Invalid", "Hash" };
        readonly string[] InvalidProductMessage = { "Invalid", "Product" };
        readonly string[] InvalidTimestampMessage = { "Invalid", "Time" };
        readonly string[] DeserializationExceptionMessage = { "Deserialization", "Exception" };
        readonly string[] FreeMessage = { "!!! You win !!!", "!!! Party time !!!" };

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
            Melody = ErrorMelody;
            Message = new[] { "Invalid Badge", badgeId };
            Time = DateTime.Now.ToUnixTimestamp();
            Hash = GenerateHash();
        }
        
        public BuyResponse(BuyResponseStatus status)
        {
            switch (status)
            {
                case BuyResponseStatus.InsufficientFunds:
                    Melody = ErrorMelody;
                    Message = InsufficientFundsMessage;
                    break;
                case BuyResponseStatus.InvalidHash:
                    Melody = ErrorMelody;
                    Message = InvalidHashMessage;
                    break;
                case BuyResponseStatus.InvalidProduct:
                    Melody = ErrorMelody;
                    Message = InvalidProductMessage;
                    break;
                case BuyResponseStatus.InvalidTimestamp:
                    Melody = ErrorMelody;
                    Message = InvalidTimestampMessage;
                    break;
                case BuyResponseStatus.DeserializationException:
                    Melody = ErrorMelody;
                    Message = DeserializationExceptionMessage;
                    break;
                case BuyResponseStatus.Free:
                    Melody = FreeMelody;
                    Message = FreeMessage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status", "Valid and Invalid Badge responses must be generated with the appropriate constructor.");
            }

            Time = DateTime.Now.ToUnixTimestamp();
            Hash = GenerateHash();
        }

        static string GetMelody(int productId)
        {
            return (!ProductMelodies.ContainsKey(productId)) ? ProductMelodies[-1] : ProductMelodies[productId];
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