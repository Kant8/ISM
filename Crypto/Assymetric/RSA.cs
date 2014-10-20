using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Crypto.Helpers;

namespace Crypto.Assymetric
{
    public class RSA : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            var blocks = BlockHelper.SplitInBlocks(message);

            var encodedBlocks = blocks.Select(EncodeBlock).ToArray();

            var encodedMessage = BlockHelper.CombineBlocks(encodedBlocks);

            return encodedMessage;
        }

        public byte[] Decode(byte[] message)
        {
            var blocks = BlockHelper.SplitInBlocks(message);

            var decodedBlocks = blocks.Select(DecodeBlock).ToArray();

            var decodedMessage = BlockHelper.CombineBlocks(decodedBlocks);

            return decodedMessage;
        }

        public ICryptoKey CryptoKey
        {
            get { return RsaKey; }
        }

        public RsaKey RsaKey { get; set; }

        public void GenerateCryptoKey()
        {
            RsaKey = new RsaKey();

            var png = new PrimeNumberGenerator();
            var p = png.NextPrimeBigInteger();
            var q = png.NextPrimeBigInteger();

            var n = p*q;

            var eilerF = (p - 1)*(q - 1);

            const int e = 65537;

            var d = FindD(e, eilerF);

            RsaKey.D = d;
            RsaKey.E = e;
            RsaKey.N = n;
        }

        #endregion ICryptoCoder

        private BigInteger FindD(BigInteger e, BigInteger eilerF)
        {
            BigInteger a = e;
            BigInteger b = eilerF;
            BigInteger x = 1;
            BigInteger d = a;
            BigInteger v1 = 0;
            BigInteger v3 = b;

            while (v3 > 0)
            {
                BigInteger q0 = d / v3;
                BigInteger q1 = d % v3;
                BigInteger tmp = v1 * q0;
                BigInteger tn = x - tmp;
                x = v1;
                v1 = tn;

                d = v3;
                v3 = q1;
            }

            //BigInteger tmp2 = x * (a);
            //tmp2 = d - (tmp2);
            //BigInteger res = tmp2 / (b);

            return x;
        }

        private UInt64 EncodeBlock(UInt64 block)
        {
            var m = new BigInteger(block);
            var encoded = BigInteger.ModPow(m, RsaKey.E, RsaKey.N);
            
        }

        private UInt64 DecodeBlock(UInt64 block)
        {
            UInt32 b = block.HiWord();
            UInt32 a = block.LoWord();
            for (int i = 0; i < RoundsCount; i++)
            {
                var temp = a;
                a = b ^ FeistelFunc(a, _roundKeys[i]);
                b = temp;
            }
            UInt64 encodedBlock = a.Combine(b);
            return encodedBlock;
        }
    }
}
