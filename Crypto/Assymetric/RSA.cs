using System;
using System.Collections;
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

        private UInt64 EncodeBlock(UInt64 block)
        {
            var m = new BigInteger(block);
            var encoded = BigInteger.ModPow(m, RsaKey.E, RsaKey.N);
            return 0;

        }

        private UInt64 DecodeBlock(UInt64 block)
        {
            return 0;
        }

        private List<BigInteger> Split(byte[] message)
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
        /// Очень важно!
        /// Разбиваем исходный текст на блоки по log2(n) бит
        /// </summary>
        private List<BigInteger> DivideTextOnBlocks(byte[] message)
        {
            var res = new List<BigInteger>();
            //Определяем количество бит в блоке по формуле х = [log2(n)]
            _bitsPerBlock = (int)Math.Floor(BigInteger.Log(RsaKey.N, 2));
            //Определяем количество байт в блоке
            _bytesPerBlock = (int)Math.Ceiling((double)_bitsPerBlock / 8);

            //Преобразуем байты исходного текста в биты
            var sourceBits = new BitArray(message);
            //Определяем количество блоков
            _blocksCount = (int)Math.Ceiling((double)sourceBits.Count / _bitsPerBlock);

            //Определяем количество бит в последнем блоке
            var index = sourceBits.Count % _bitsPerBlock;

            //Формируем блоки
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

                //Блок бит преобразуем в блок байт
                tempBits.CopyTo(tempBytes, 0);
                //Блок байт преобразуем в число BigInteger и далее шифруем/дешифруем такие числа
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
