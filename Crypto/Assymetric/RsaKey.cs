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
            throw new NotImplementedException();
        }

        public void DecodeKeyFromString(string input)
        {
            throw new NotImplementedException();
        }

        public string EncodeKeyToString()
        {
            throw new NotImplementedException();
        }

        public string DecodeKeyToString()
        {
            throw new NotImplementedException();
        }
    }
}
