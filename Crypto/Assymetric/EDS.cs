using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Crypto.Helpers;

namespace Crypto.Assymetric
{
    public class EDS
    {
        public string CheckKey { get; set; }

        public string SignKey { get; set; }

        public string Sign(string message)
        {
            var hash = GetHash(message);

            var rsa = new RSA();
            rsa.CryptoKey.DecodeKeyFromString(SignKey);
            var sign = rsa.Decode(hash);

            return ByteArrayToString(sign);
        }

        public bool CheckSign(string message, string sign)
        {
            var rsa = new RSA();
            rsa.CryptoKey.EncodeKeyFromString(CheckKey);
            var hash = GetHash(message);

            var decodedHash = rsa.Encode(StringToByteArray(sign));
            decodedHash = decodedHash.Trim(hash.Length);
            return hash.SequenceEqual(decodedHash);
        }

        private byte[] GetHash(string message)
        {
            var hasher = HashAlgorithm.Create("SHA256");
            if (hasher == null) throw new ApplicationException();
            return hasher.ComputeHash(message.GetUtf16Bytes());
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static byte[] StringToByteArray(String hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
