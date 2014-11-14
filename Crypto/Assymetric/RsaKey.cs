using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Assymetric
{
    public class RsaKey : ICryptoKey
    {
        /// <summary>
        /// Public exponent
        /// </summary>
        public BigInteger E { get; set; }

        /// <summary>
        /// Modulus
        /// </summary>
        public BigInteger N { get; set; }

        /// <summary>
        /// Private exponent
        /// </summary>
        public BigInteger D { get; set; }

        public void EncodeKeyFromString(string input)
        {
            var splitted = input.Split('\t');
            E = new BigInteger(Convert.FromBase64String(splitted[0]));
            N = new BigInteger(Convert.FromBase64String(splitted[1]));
        }

        public void DecodeKeyFromString(string input)
        {
            var splitted = input.Split('\t');
            D = new BigInteger(Convert.FromBase64String(splitted[0]));
            N = new BigInteger(Convert.FromBase64String(splitted[1]));
        }

        public string EncodeKeyToString()
        {
            var res = new StringBuilder();
            res.Append(Convert.ToBase64String(E.ToByteArray()));
            res.Append('\t');
            res.Append(Convert.ToBase64String(N.ToByteArray()));
            return res.ToString();
        }

        public string DecodeKeyToString()
        {
            var res = new StringBuilder();
            res.Append(Convert.ToBase64String(D.ToByteArray()));
            res.Append('\t');
            res.Append(Convert.ToBase64String(N.ToByteArray()));
            return res.ToString();
        }
    }
}
