using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Crypto.Helpers;

namespace Crypto.Assymetric
{
    public class RSA : ICryptoCoder
    {
        private int _bitsPerBlock;
        private int _bytesPerBlock;
        private int _blocksCount;

        public RSA()
        {
            RsaKey = new RsaKey();
        }

        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            //var block = new BigInteger(message);
            //if (block >= RsaKey.N)
            //    throw new ArgumentException("Текст слишком велик");
            //var encodedBlock = EncodeBlock(block);
            //return encodedBlock.ToByteArray();

            var blocks = SplitInBlocks(message);

            var encodedBlocks = blocks.Select(EncodeBlock).ToList();

            var encodedMessage = CombineBlocks(encodedBlocks);

            return encodedMessage;
        }
        
        public byte[] Decode(byte[] message)
        {
            //var block = new BigInteger(message);
            //if (block >= RsaKey.N)
            //    throw new ArgumentException("Текст слишком велик");
            //var decodedBlock = DecodeBlock(block);
            //return decodedBlock.ToByteArray();

            var blocks = SplitInBlocks(message);

            var decodedBlocks = blocks.Select(DecodeBlock).ToList();

            var decodedMessage = CombineBlocks(decodedBlocks);

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
            png = new PrimeNumberGenerator();
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
            BigInteger b = eilerF, x = BigInteger.Zero, d = BigInteger.One;

            while (e.CompareTo(BigInteger.Zero) == 1) // e > 0
            {
                var q = BigInteger.Divide(b, e);
                var y = e;
                e = BigInteger.ModPow(b, 1, e);
                b = y;
                y = d;
                d = BigInteger.Subtract(x, BigInteger.Multiply(q, d));
                x = y;
            }
            x = BigInteger.ModPow(x, 1, eilerF);

            if (x.CompareTo(BigInteger.Zero) == -1)// x < 0
            {
                x = BigInteger.ModPow(BigInteger.Add(x, eilerF), 1, eilerF);
            }

            return x;
        }

        private BigInteger EncodeBlock(BigInteger block)
        {
            var encoded = BigInteger.ModPow(block, RsaKey.E, RsaKey.N);
            return encoded;

        }

        private BigInteger DecodeBlock(BigInteger block)
        {
            var decoded = BigInteger.ModPow(block, RsaKey.D, RsaKey.N);
            return decoded;
        }

        private List<BigInteger> SplitInBlocksOLD(byte[] message)
        {
            var bitLength = (int) Math.Ceiling(BigInteger.Log(RsaKey.N, 2));
            bitLength--;

            var bytesLength = bitLength/8 + ((bitLength%8 == 0) ? 0 : 1);

            // mb remove reverse
            var messageBits = new BitArray(message.Reverse().ToArray());

            var result = new List<BigInteger>();

            int offset = 0;
            while (offset * 8 < messageBits.Length) //fix this
            {
                var number = new List<byte>(bytesLength);
                for (int i = 0; i < bitLength; i+= 8)
                {
                    byte singleByte = 0;
                    if (i + 8 < bitLength)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            var bit = messageBits.Get(offset + i + j);
                            BitHelper.SetBit(ref singleByte, j, bit);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < bitLength - i; j++)
                        {
                            var bit = messageBits.Get(offset + i + j);
                            BitHelper.SetBit(ref singleByte, j, bit);
                        }
                    }
                    number.Add(singleByte);
                }
                offset += bitLength;
                result.Add(new BigInteger(number.ToArray()));
            }
            return result;
        }

        #region Common

        /// <summary>
        /// split per log2(n)
        /// </summary>
        private List<BigInteger> SplitInBlocks(byte[] message)
        {
            var res = new List<BigInteger>();
            _bitsPerBlock = (int)Math.Floor(BigInteger.Log(RsaKey.N, 2));
            _bytesPerBlock = (int)Math.Ceiling((double)_bitsPerBlock / 8);

            var sourceBits = new BitArray(message);
            _blocksCount = (int)Math.Ceiling((double)sourceBits.Count / _bitsPerBlock);

            var index = sourceBits.Count % _bitsPerBlock;

            for (var i = 0; i < _blocksCount; i++)
            {
                var tempBits = new BitArray(_bitsPerBlock);
                var tempBytes = new byte[_bytesPerBlock + 1];

                if ((i == _blocksCount - 1) && (index != 0))
                {
                    for (var j = 0; j < index; j++)
                    {
                        tempBits[j] = sourceBits[i * _bitsPerBlock + j];
                    }
                }
                else
                {
                    for (var j = 0; j < _bitsPerBlock; j++)
                    {
                        tempBits[j] = sourceBits[i * _bitsPerBlock + j];
                    }
                }

                tempBits.CopyTo(tempBytes, 0);
                res.Add(new BigInteger(tempBytes));
            }
            return res;
        }

        /// <summary>
        /// Объединяем блоки BigIntegeroв по log2(n) бит в текст
        /// </summary>
        private byte[] CombineBlocks(List<BigInteger> blocks)
        {
            var textBits = new BitArray(_blocksCount * _bitsPerBlock);

            var byteCount = (int)Math.Ceiling((double)(_blocksCount * _bitsPerBlock) / 8);
            var textBytes = new byte[byteCount];

            for (var i = 0; i < _blocksCount; i++)
            {
                var bigInteger = blocks[i];

                var tempBytes = bigInteger.ToByteArray().ToList();
                var tempBytesCount = tempBytes.Count;
                for (var j = 0; j < _bytesPerBlock - tempBytesCount; j++)
                {
                    tempBytes.Add(0);
                }
                var tempBits = new BitArray(tempBytes.ToArray());

                for (var j = 0; j < _bitsPerBlock; j++)
                {
                    textBits[i * _bitsPerBlock + j] = tempBits[j];
                }
            }

            textBits.CopyTo(textBytes, 0);

            return textBytes;
        }

        #endregion
    }
}
