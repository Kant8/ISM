using System;
using System.Text;
using Crypto.Helpers;

namespace Crypto.Shift
{
    public class Caesar : ICryptoCoder
    {
        public const int AlphabetSize = char.MaxValue - char.MinValue + 1;

        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            var chars = Encoding.Unicode.GetChars(message);
            var shift = GetShift();

            var encodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var encodedC = chars[i] + shift;
                encodedMessage[i] = (char)encodedC.Mod(AlphabetSize);
            }
            return Encoding.Unicode.GetBytes(encodedMessage);
        }

        public byte[] Decode(byte[] message)
        {
            var chars = Encoding.Unicode.GetChars(message);
            var shift = GetShift();

            var decodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var decodedC = chars[i] - shift;
                decodedMessage[i] = (char)decodedC.Mod(AlphabetSize);
            }
            return Encoding.Unicode.GetBytes(decodedMessage);
        }

        #endregion ICryptoCoder

        private int GetShift()
        {
            int shift;
            if (Int32.TryParse(Key, out shift))
                return shift;
            throw new ArgumentException("Key should be int.");
        }
    }
}
