using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public class Caesar : ICryptoCoder
    {
        public string Encode(string message, object key)
        {
            var chars = message.ToCharArray();
            var shift = (int)key;

            var encodedMessage = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                var encodedC = chars[i] + shift;
                encodedMessage[i] = (char)encodedC;
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
                decodedMessage[i] = (char)decodedC;
            }
            return decodedMessage.ToCharString();
        }
    }
}
