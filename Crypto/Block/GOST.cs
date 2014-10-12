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

        public byte[] PublicKey { get; set; }
        public byte[] PrivateKey { get; set; }
        public Tuple<byte[], byte[]> GenerateKeys()
        {
            throw new NotImplementedException();
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
            UInt32 summ = subBlock.AddWithMod2Pow32(roundKey);
            UInt32 substituted = Substitution(summ);
            UInt32 rotated = substituted.RotateLeft(11);
            return rotated;
        }

        private UInt32 Substitution(UInt32 block)
        {
            UInt32 result = 0;

            const int subBlockSize = 4;
            const int subBlocksCount = 8;
            for (var subBlockIndex = 0; subBlockIndex < subBlocksCount; subBlockIndex++)
            {
                Byte subBlock = 0;
                for (int i = 0; i < subBlockSize; i++)
                {
                    var bit = block.GetBit(subBlockIndex * subBlockSize + i);
                    BitHelper.SetBit(ref subBlock, i, bit);
                }

                var sRes = SBlocks[subBlocksCount - subBlockIndex - 1][subBlock];

                for (int i = 0; i < subBlockSize; i++)
                {
                    var bit = sRes.GetBit(i);
                    BitHelper.SetBit(ref result, subBlockIndex * subBlockSize + i, bit);
                }
            }
            return result;
        }

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
