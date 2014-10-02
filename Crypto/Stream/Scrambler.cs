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

        #endregion ICryptoCoder

        private uint GetSeed()
        {
            uint seed;
            if (UInt32.TryParse(Key, out seed))
                return seed;
            throw new ArgumentException("Key should be uint.");
        }

        public void Test(string message)
        {
            var seed = GetSeed();
            var lfsr = new LinearFeedbackShiftRegister(seed);


            var messageBytes = Encoding.UTF8.GetBytes(message);
            var bits = new BitArray(messageBytes);
            var encodedBits = new BitArray(bits.Length);

            for (int i = 0; i < bits.Length; i++)
            {
                encodedBits[i] = bits[i] ^ lfsr.NextBit();
            }

            var encodedMessageBytes = new byte[messageBytes.Length];
            encodedBits.CopyTo(encodedMessageBytes, 0);

            var message2 = Encoding.UTF8.GetString(encodedMessageBytes);//.ToCharString();







            var seed2 = GetSeed();
            var lfsr2 = new LinearFeedbackShiftRegister(seed2);

            var messageBytes2 = Encoding.UTF8.GetBytes(message2);

            for (int i = 0; i < messageBytes2.Length; i++)
            {
                bool res = encodedMessageBytes[i] == messageBytes2[i];
            }


            var bits2 = new BitArray(messageBytes2);
            var decodedBits2 = new BitArray(bits2.Length);

            for (int i = 0; i < bits2.Length; i++)
            {
                decodedBits2[i] = bits2[i] ^ lfsr2.NextBit();
            }

            var decodedMessageBytes2 = new byte[messageBytes2.Length];
            decodedBits2.CopyTo(decodedMessageBytes2, 0);

            var decodedMessage = Encoding.UTF8.GetChars(decodedMessageBytes2).ToCharString();
        }

    }
}
