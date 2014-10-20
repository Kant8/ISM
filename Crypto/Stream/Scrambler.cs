using System;
using System.Collections;
using System.Text;
using Crypto.Helpers;

namespace Crypto.Stream
{
    public class Scrambler : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            var seed = GetSeed();
            var lfsr = new LinearFeedbackShiftRegister(seed);

            var bits = new BitArray(message);
            var encodedBits = new BitArray(bits.Length);

            for (int i = 0; i < bits.Length; i++)
            {
                encodedBits[i] = bits[i] ^ lfsr.NextBit();
            }

            var encodedMessageBytes = new byte[message.Length];
            encodedBits.CopyTo(encodedMessageBytes, 0);

            return encodedMessageBytes;
        }

        public byte[] Decode(byte[] message)
        {
            var seed = GetSeed();
            var lfsr = new LinearFeedbackShiftRegister(seed);

            var bits = new BitArray(message);
            var decodedBits = new BitArray(bits.Length);

            for (int i = 0; i < bits.Length; i++)
            {
                decodedBits[i] = bits[i] ^ lfsr.NextBit();
            }

            var decodedMessageBytes = new byte[message.Length];
            decodedBits.CopyTo(decodedMessageBytes, 0);

            return decodedMessageBytes;
        }

        public ICryptoKey CryptoKey { get; set; }

        public void GenerateCryptoKey()
        {
            throw new NotImplementedException();
        }

        #endregion ICryptoCoder

        private uint GetSeed()
        {
            uint seed;
            if (UInt32.TryParse(Key, out seed))
                return seed;
            throw new ArgumentException("Key should be uint.");
        }
    }
}
