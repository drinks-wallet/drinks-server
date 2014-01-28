using System;
using System.Linq;
using System.Security;

namespace BrandonHaynes.Security.SipHash
{
    [SecurityCritical]
    internal class SipState
    {
        private int CompressionRounds { get; set; }
        private int FinalizationRounds { get; set; }

        private readonly ulong[] _v = new ulong[4];

        internal SipState(byte[] key, int compressionRounds, int finalizationRounds)
        {
            SipHash.ValidateArguments(key, compressionRounds, finalizationRounds);

            var k0 = BitConverter.ToUInt64(key, 0);
            var k1 = BitConverter.ToUInt64(key, 8);

            // "somepseudorandomlychosenbytes".  See §4
            _v[0] = 0x736f6d6570736575UL ^ k0;
            _v[1] = 0x646f72616e646f6dUL ^ k1;
            _v[2] = 0x6c7967656e657261UL ^ k0;
            _v[3] = 0x7465646279746573UL ^ k1;

            CompressionRounds = compressionRounds;
            FinalizationRounds = finalizationRounds;
        }

        internal void Process(ulong word)
        {
            // §2, fig 2.2
            _v[3] ^= word;
            CompressionRounds.Repeat(SipRound);
            _v[0] ^= word;
        }

        internal ulong Finalization()
        {
            // §2.3
            _v[2] ^= 0xff;
            FinalizationRounds.Repeat(SipRound);
            return _v.Aggregate(0UL, (a, v) => a ^ v);
        }

        private void SipRound()
        {
            // §2
            _v[0] += _v[1];
            _v[2] += _v[3];

            _v[1] = _v[1].RotateLeft(13);
            _v[3] = _v[3].RotateLeft(16);

            _v[1] ^= _v[0];
            _v[3] ^= _v[2];

            _v[0] = _v[0].RotateLeft(32);

            _v[2] += _v[1];
            _v[0] += _v[3];

            _v[1] = _v[1].RotateLeft(17);
            _v[3] = _v[3].RotateLeft(21);

            _v[1] ^= _v[2];
            _v[3] ^= _v[0];

            _v[2] = _v[2].RotateLeft(32);
        }
    }
}