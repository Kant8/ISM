namespace Crypto.Caesar
{
    public class Caesar : ICryptoCoder
    {
        public const int AlphabetSize = char.MaxValue - char.MinValue + 1;

        public string Encode(string message, object key)
        {
            var chars = message.ToCharArray();
            var shift = (int)key;

            var encodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var encodedC = chars[i] + shift;
                encodedMessage[i] = (char)encodedC.Mod(AlphabetSize);
            }
            return encodedMessage.ToCharString();
        }

        public string Decode(string message, object key)
        {
            var chars = message.ToCharArray();
            var shift = (int)key;

            var decodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var decodedC = chars[i] - shift;
                decodedMessage[i] = (char)decodedC.Mod(AlphabetSize);
            }
            return decodedMessage.ToCharString();
        }
    }
}
