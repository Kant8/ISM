namespace Crypto.Caesar
{
    public class Caesar : ICryptoCoder
    {
        public const int AlphabetSize = char.MaxValue - char.MinValue + 1;

        #region ICryptoCoder

        public object Key { get; set; }

        public string Encode(string message)
        {
            var chars = message.ToCharArray();
            var shift = (int)Key;

            var encodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var encodedC = chars[i] + shift;
                encodedMessage[i] = (char)encodedC.Mod(AlphabetSize);
            }
            return encodedMessage.ToCharString();
        }

        public string Decode(string message)
        {
            var chars = message.ToCharArray();
            var shift = (int)Key;

            var decodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var decodedC = chars[i] - shift;
                decodedMessage[i] = (char)decodedC.Mod(AlphabetSize);
            }
            return decodedMessage.ToCharString();
        }

        #endregion ICryptoCoder
    }
}
