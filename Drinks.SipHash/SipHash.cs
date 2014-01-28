using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;

namespace BrandonHaynes.Security.SipHash
{
    public sealed class SipHash : KeyedHashAlgorithm
    {
        // Minimum rounds for PRF security per §3
        public const int DefaultCompressionRounds = 2;
        public const int DefaultFinalizationRounds = 4;

        public int CompressionRounds { get; private set; }
        public int FinalizationRounds { get; private set; }

        [SecurityCritical]
        private SipState _state;

        // Remaining unprocessed in our stream
        [SecurityCritical]
        private byte[] _remainder;
        // Total bytes encountered so far
        private ulong _totalBytes;

        public SipHash(byte[] key, int compressionRounds, int finalizationRounds)
        {
            Key = key;
            CompressionRounds = compressionRounds;
            FinalizationRounds = finalizationRounds;

            ValidateArguments(key, compressionRounds, finalizationRounds);
            Initialize();
        }

        public SipHash(params byte[] key)
            : this(key, DefaultCompressionRounds, DefaultFinalizationRounds)
        { }

        #region Overrides of HashAlgorithm

        public override void Initialize()
        {
            _state = new SipState(Key, CompressionRounds, FinalizationRounds);
            _totalBytes = 0;
            _remainder = new byte[0];
        }

        protected override void HashCore(byte[] message, int offset, int length)
        {
            if (message == null) throw new ArgumentNullException("message");

            // Process blocks of 8 bytes each, see §2.2
            var words = (_remainder.Length + length) / 8; // 64-bit blocks
            // Join any remainder from a previous chunk with the new data
            var data = _remainder.Concat(message.ArraySkip(offset).Take(length));
            // Begin enumeration of our resulting bytestream
            var iterator = data.GetEnumerator();

            // For each word w, get it and process (see §2.2)
            words.Repeat(() => _state.Process(iterator.NextWord()));

            // We may have one or more remaining bytes; these may be prepended
            // to a subsequent chunk of data or used as a final remainder below
            _remainder = iterator.ToEnumerable().ToArray();
            _totalBytes += (ulong)length;
        }

        protected override byte[] HashFinal()
        {
            // Remainder and (total bytes mod 256 into high 8 bits of 64-bit ulong).
            // See §2.3.
            var finalWord = BitConverter.ToUInt64(_remainder.Extend(8), 0) | (_totalBytes << 56);

            // Process our final word, execute finalization per §2.3, and return a result
            _state.Process(finalWord);
            return BitConverter.GetBytes(_state.Finalization());
        }

        #endregion

        internal static void ValidateArguments(ICollection<byte> key, int compressionRounds, int finalizationRounds)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            else if (key.Count != 16)
                throw new ArgumentException("Expected a byte stream of length 16.", "key");

            if (compressionRounds < 2)
                throw new ArgumentException("Expected at least c>=2 compression rounds; see §3", "compressionRounds");
            else if (finalizationRounds < 2)
                throw new ArgumentException("Expected at least d>=4 finalization rounds; see §3", "finalizationRounds");
        }
    }
}