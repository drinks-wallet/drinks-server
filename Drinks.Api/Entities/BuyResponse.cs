using Drinks.SipHasher;

namespace Drinks.Api.Entities
{
    using System;
    using Drinks.Entities.Extensions;

    public class BuyResponse : TerminalResponseBase
    {
        public BuyResponse()
        {

        }

        public BuyResponse(BuyResponseStatus status, string name = null, string balance = null)
        {
            switch (status)
            {
                case BuyResponseStatus.Valid:
                    Melody = "a1b1c1d1e1f1g1";
                    Message = new[] { RemoveDiacritics(name), balance };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.InsufficientFunds:
                    Melody = "c5";
                    Message = new[] { "Insufficient", "Funds" };
                    Time = DateTime.Now.ToUnixTimestamp();
                    Hash = GenerateHash();
                    break;
                case BuyResponseStatus.InvalidBadge:
                    Melody = "c5";
                    Message = new[] { "Invalid", "Badge" };
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
                default:
                    throw new ArgumentOutOfRangeException("requestStatus");
            }
        }

        public string Melody { get; set; }
        public string[] Message { get; set; }
        public int Time { get; set; }
        public string Hash { get; set; }

        string GenerateHash()
        {
            var messageString = string.Join(string.Empty, Message);
            var message = Melody + messageString + Time;
            var hasher = new DrinksSipHasher();
            return hasher.CalculateHash(message, ConfigurationFacade.LocalHashKey);
        }
    }
}