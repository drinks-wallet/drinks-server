using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using BrandonHaynes.Security.SipHash;

namespace Drinks.SipHasher
{
    public interface IDrinksSipHasher
    {
        string CalculateHash(string message, string littleEndianKey);
    }

    public class DrinksSipHasher : IDrinksSipHasher
    {
        const int HashLength = 16;

        public string CalculateHash(string message, string littleEndianKey)
        {
            var messageBytes = Encoding.ASCII.GetBytes(message);
            var byteKey = BigInteger.Parse(littleEndianKey, NumberStyles.HexNumber).ToByteArray();
            var hasher = new SipHash(byteKey);
            var hash = hasher.ComputeHash(messageBytes);
            var stringHash = BitConverter.ToUInt64(hash, 0).ToString("X");
            return PadHash(stringHash);
        }

        static string PadHash(string hash)
        {
            var padLength = HashLength - hash.Length;
            return padLength == 0 ? hash : new string('0', padLength) + hash;
        }
    }
}
