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

        private const int RoundsCount = 16;

        private List<byte[]> _roundKeys;

        private UInt64 EncodeBlock(UInt64 block)
        {
            var left = block.HiWord();
            var right = block.LoWord();
            for (int i = 0; i < RoundsCount; i++)
            {
                var temp = right;
                right = left ^ FeistelFunc(right, _roundKeys[i]);
                left = temp;
            }
            return left.Combine(right);
        }

        private UInt64 DecodeBlock(UInt64 block)
        {
            var left = block.HiWord();
            var right = block.LoWord();
            for (int i = RoundsCount - 1; i >= 0; i--)
            {
                var temp = left;
                left = right ^ FeistelFunc(left, _roundKeys[i]);
                right = temp;
            }
            return left.Combine(right);
        }

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

        private UInt32 FeistelFunc(UInt32 subBlock, byte[] roundKey)
        {
            var key = BitConverter.ToUInt32(roundKey, 0);
            return subBlock ^ key;
        }
    }
}
