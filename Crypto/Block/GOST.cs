using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Crypto.Helpers;

namespace Crypto.Block
{
    public class GOST : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key, true);

            var blocks = BlockHelper.SplitInBlocks(message);

            var encodedBlocks = blocks.Select(EncodeBlock).ToArray();

            var encodedMessage = BlockHelper.CombineBlocks(encodedBlocks);

            return encodedMessage;
        }

        public byte[] Decode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key, false);

            var blocks = BlockHelper.SplitInBlocks(message);

            var decodedBlocks = blocks.Select(DecodeBlock).ToArray();

            var decodedMessage = BlockHelper.CombineBlocks(decodedBlocks);

            return decodedMessage;
        }

        #endregion ICryptoCoder

        private const int RoundsCount = 32;
        private List<UInt32> _roundKeys;

        private UInt64 EncodeBlock(UInt64 block)
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

        #region Feistel Function

        private UInt32 FeistelFunc(UInt32 subBlock, UInt32 roundKey)
        {
            UInt64 expanded = BlockHelper.PermutateBlock(subBlock, ETable);
            UInt64 xored = expanded ^ roundKey;
            UInt32 substituted = Substitution(xored);
            UInt32 permutated = (UInt32)BlockHelper.PermutateBlock(substituted, PTable);
            return permutated;
        }

        private static readonly byte[] ETable =
        {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1,
        };

        private UInt32 Substitution(UInt64 block)
        {
            UInt32 result = 0;

            const int subBlockResultSize = 4;
            const int subBlockSize = 6;
            const int subBlocksCount = 8;
            for (var subBlockIndex = 0; subBlockIndex < subBlocksCount; subBlockIndex++)
            {
                Byte subBlock = 0;
                for (int i = 0; i < subBlockSize; i++)
                {
                    var bit = block.GetBit(subBlockIndex * subBlockSize + i);
                    BitHelper.SetBit(ref subBlock, i, bit);
                }

                byte sRowIndex = 0;
                BitHelper.SetBit(ref sRowIndex, 0, subBlock.GetBit(0));
                BitHelper.SetBit(ref sRowIndex, 1, subBlock.GetBit(5));

                byte sColIndex = 0;
                for (int i = 0; i < subBlockResultSize; i++)
                {
                    BitHelper.SetBit(ref sColIndex, i, subBlock.GetBit(i + 1));
                }

                var sRes = SBlocks[subBlocksCount - subBlockIndex - 1][sRowIndex, sColIndex];

                for (int i = 0; i < subBlockResultSize; i++)
                {
                    var bit = sRes.GetBit(i);
                    BitHelper.SetBit(ref result, subBlockIndex * subBlockResultSize + i, bit);
                }
            }
            return result;
        }

        private static readonly byte[] PTable =
        {
            16, 7, 20, 21, 29, 12, 28, 17,
            1, 15, 23, 26, 5, 18, 31, 10,
            2, 8, 24, 14, 32, 27, 3, 9,
            19, 13, 30, 6, 22, 11, 4, 25,
        };

        #endregion Feistel Function

        #region Round Keys Generating

        private List<UInt32> GenerateRoundKeys(string key, bool encode)
        {
            BigInteger intKey = BigInteger.Parse(key);
            var bytesOfKey = intKey.ToByteArray();

            if (bytesOfKey.Length > 32)
                throw new ArgumentException("Ключ длиннее 256 бит");
            if (bytesOfKey.Length < 32)
            {
                var zeroBytes = new byte[32 - bytesOfKey.Length];
                bytesOfKey = zeroBytes.Concat(bytesOfKey).ToArray();
            }

            var keys1To8 = new List<UInt32>(8);
            for (int i = 0; i < bytesOfKey.Length; i += 4)
            {
                keys1To8.Add(BitConverter.ToUInt32(bytesOfKey, i));
            }
            keys1To8.Reverse(); // todo: check if reverse is needed

            var roundKeys = new List<UInt32>(RoundsCount);
            if (encode)
            {
                roundKeys.AddRange(keys1To8);
                roundKeys.AddRange(keys1To8);
                roundKeys.AddRange(keys1To8);
                roundKeys.AddRange(keys1To8.AsEnumerable().Reverse());
            }
            else
            {
                roundKeys.AddRange(keys1To8);
                roundKeys.AddRange(keys1To8.AsEnumerable().Reverse());
                roundKeys.AddRange(keys1To8.AsEnumerable().Reverse());
                roundKeys.AddRange(keys1To8.AsEnumerable().Reverse());
            }

            return roundKeys;
        }

        #endregion Round Keys Generating

        #region S Blocks

        private static readonly byte[] S1Block =
        { 4, 10, 9, 2, 13, 8, 0, 14, 6, 11, 1, 12, 7, 15, 5, 3 };
        
        private static readonly byte[] S2Block =
        { 14, 11, 4, 12, 6, 13, 15, 10, 2, 3, 8, 1, 0, 7, 5, 9 };

        private static readonly byte[] S3Block =
        { 5, 8, 1, 13, 10, 3, 4, 2, 14, 15, 12, 7, 6, 0, 9, 11 };

        private static readonly byte[] S4Block =
        { 7, 13, 10, 1, 0, 8, 9, 15, 14, 4, 6, 12, 11, 2, 5, 3 };

        private static readonly byte[] S5Block =
        { 6, 12, 7, 1, 5, 15, 13, 8, 4, 10, 9, 14, 0, 3, 11, 2 };

        private static readonly byte[] S6Block =
        { 4, 11, 10, 0, 7, 2, 1, 13, 3, 6, 8, 5, 9, 12, 15, 14 };

        private static readonly byte[] S7Block =
        { 13, 11, 4, 1, 3, 15, 5, 9, 0, 10, 14, 7, 6, 8, 2, 12 };

        private static readonly byte[] S8Block =
        { 1, 15, 13, 0, 5, 7, 10, 4, 9, 2, 3, 14, 6, 11, 8, 12 };

        private static readonly List<byte[]> SBlocks = new List<byte[]>
        {
            S1Block, S2Block, S3Block, S4Block,
            S5Block, S6Block, S7Block, S8Block
        };

        #endregion S Blocks
    }
}
