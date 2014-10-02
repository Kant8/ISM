using System;
using System.Collections.Generic;
using System.Linq;
using Crypto.Helpers;

namespace Crypto.Block
{
    public class Feistel : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key);

            var blocks = BlockHelper.SplitInBlocks(message);

            var encodedBlocks = blocks.Select(EncodeBlock).ToArray();

            var encodedMessage = BlockHelper.CombineBlocks(encodedBlocks);

            return encodedMessage;
        }

        public byte[] Decode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key);

            var blocks = BlockHelper.SplitInBlocks(message);

            var decodedBlocks = blocks.Select(DecodeBlock).ToArray();

            var decodedMessage = BlockHelper.CombineBlocks(decodedBlocks);

            return decodedMessage;
        }

        #endregion ICryptoCoder

        public Feistel()
        {
            RoundsCount = 16;
            RoundKeyGenFunc = GenerateRoundKeys;
            CryptoFunc = EncryptSubBlock;
        }

        public Feistel(int roundsCount, Func<UInt32, byte[], UInt32> cryptoFunc, Func<string, List<byte[]>> roundKeyGenFunc)
        {
            RoundsCount = roundsCount;
            CryptoFunc = cryptoFunc;
            RoundKeyGenFunc = roundKeyGenFunc;
        }

        public int RoundsCount;

        /// <summary>
        /// block, key, result block
        /// </summary>
        public Func<UInt32, byte[], UInt32> CryptoFunc;

        /// <summary>
        /// initial key, result round keys
        /// </summary>
        public Func<string, List<byte[]>> RoundKeyGenFunc;

        private List<byte[]> _roundKeys;

        private UInt64 EncodeBlock(UInt64 block)
        {
            var left = block.HiWord();
            var right = block.LoWord();
            uint temp;
            for (int i = 0; i < RoundsCount; i++)
            {
                temp = left ^ CryptoFunc(right, _roundKeys[i]);
                right = left;
                left = temp;
            }
            temp = left;
            left = right;
            right = temp;
            return left.Combine(right);
        }

        private UInt64 DecodeBlock(UInt64 block)
        {
            var left = block.HiWord();
            var right = block.LoWord();
            uint temp;
            for (int i = RoundsCount - 1; i >= 0; i--)
            {
                temp = left ^ CryptoFunc(right, _roundKeys[i]);
                right = left;
                left = temp;
            }
            temp = left;
            left = right;
            right = temp;
            return left.Combine(right);
        }

        #region Base Functions

        private List<byte[]> GenerateRoundKeys(string key)
        {
            var intKey = UInt32.Parse(key);

            var roundKeys = new List<byte[]>(RoundsCount);
            for (int i = 0; i < RoundsCount; i++)
            {
                roundKeys.Add(BitConverter.GetBytes(intKey.RotateLeft((byte)i)));
            }
            return roundKeys;
        }

        private UInt32 EncryptSubBlock(UInt32 subBlock, byte[] roundKey)
        {
            var key = BitConverter.ToUInt32(roundKey, 0);
            return subBlock ^ key;
        }

        #endregion Base Functions
    }
}
