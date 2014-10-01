﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Block
{
    //17687699641057293660
    public class Des : ICryptoCoder
    {
        #region ICryptoCoder

        public string Key { get; set; }

        public byte[] Encode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key);

            var blocks = Helper.SplitInBlocks(message);

            var encodedBlocks = blocks.Select(EncodeBlock).ToArray();

            var encodedMessage = Helper.CombineBlocks(encodedBlocks);

            return encodedMessage;
        }

        public byte[] Decode(byte[] message)
        {
            _roundKeys = GenerateRoundKeys(Key);

            var blocks = Helper.SplitInBlocks(message);

            var decodedBlocks = blocks.Select(DecodeBlock).ToArray();

            var decodedMessage = Helper.CombineBlocks(decodedBlocks);

            return decodedMessage;
        }

        #endregion ICryptoCoder

        private const int RoundsCount = 16;
        private List<UInt64> _roundKeys;

        private UInt64 EncodeBlock(UInt64 block)
        {
            UInt64 ipBlock = InitialPermutation(block);

            var left = ipBlock.HiWord();
            var right = ipBlock.LoWord();
            uint temp;
            for (int i = 0; i < RoundsCount; i++)
            {
                temp = left ^ FeistelFunc(right, _roundKeys[i]);
                right = left;
                left = temp;
            }
            temp = left;
            left = right;
            right = temp;

            UInt64 feistelBlock = left.Combine(right);

            UInt64 encodedBlock = InitialPermutationReverse(feistelBlock);
            return encodedBlock;
        }

        private UInt64 DecodeBlock(UInt64 block)
        {
            UInt64 ipBlock = InitialPermutationReverse(block);

            var left = ipBlock.HiWord();
            var right = ipBlock.LoWord();
            uint temp;
            for (int i = RoundsCount - 1; i >= 0; i--)
            {
                temp = left ^ FeistelFunc(right, _roundKeys[i]);
                right = left;
                left = temp;
            }
            temp = left;
            left = right;
            right = temp;

            UInt64 feistelBlock = left.Combine(right);

            UInt64 encodedBlock = InitialPermutation(feistelBlock);
            return encodedBlock;
        }

        #region IP

        private static readonly byte[] IpTable =
        {
            58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
        };

        private UInt64 InitialPermutation(UInt64 block)
        {
            UInt64 resultBlock = 0;
            for (int i = 0; i < IpTable.Length; i++)
            {
                var bit = block.GetBit(IpTable[i] - 1);
                Helper.SetBit(ref resultBlock, i, bit);
            }
            return resultBlock;
        }

        #endregion IP

        #region Feistel Function

        private UInt32 FeistelFunc(UInt32 subBlock, UInt64 roundKey)
        {
            UInt64 expanded = Expansion(subBlock);
            UInt64 xored = expanded ^ roundKey;
            UInt32 sblocked = Substitution(xored);
            UInt32 permutated = Permutation(sblocked);
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

        private UInt64 Expansion(UInt32 block)
        {
            UInt64 resultBlock = 0;
            for (int i = 0; i < ETable.Length; i++)
            {
                var bit = block.GetBit(ETable[i] - 1);
                Helper.SetBit(ref resultBlock, i, bit);
            }
            return resultBlock;
        }

        #region S Blocks

        private static readonly byte[,] S1Block =
        {
            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
            {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
            {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
            {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
        };

        private static readonly byte[,] S2Block =
        {
            {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
            {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 4, 10, 6, 9, 11, 5},
            {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
            {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
        };

        private static readonly byte[,] S3Block =
        {
            {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
            {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
            {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
            {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
        };

        private static readonly byte[,] S4Block =
        {
            {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
            {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
            {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
            {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
        };

        private static readonly byte[,] S5Block =
        {
            {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
            {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
            {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
            {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
        };

        private static readonly byte[,] S6Block =
        {
            {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
            {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
            {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
            {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
        };

        private static readonly byte[,] S7Block =
        {
            {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
            {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
            {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
            {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}
        };

        private static readonly byte[,] S8Block =
        {
            {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
            {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
            {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
            {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
        };

        private static readonly List<byte[,]> SBlocks = new List<byte[,]>
        {
            S1Block, S2Block, S3Block, S4Block,
            S5Block, S6Block, S7Block, S8Block
        };

        #endregion S Blocks

        private UInt32 Substitution(UInt64 block)
        {
            UInt32 result = 0;

            const int subBlockResultSize = 4;
            const int subBlockSize = 6;
            const int subBlocksCount = 8;
            for (var subBlockIndex = 0; subBlockIndex < subBlocksCount; subBlockIndex++)
            {
                byte sRowIndex = 0;
                Helper.SetBit(ref sRowIndex, 0, block.GetBit(subBlockIndex*subBlockSize));
                Helper.SetBit(ref sRowIndex, 1, block.GetBit(subBlockIndex * subBlockSize + subBlockSize - 1));

                byte sColIndex = 0;
                for (int i = 1; i < subBlockSize - 1; i++)
                {
                    Helper.SetBit(ref sColIndex, i - 1, block.GetBit(subBlockIndex * subBlockSize + i));
                }

                var sRes = SBlocks[subBlocksCount - subBlockIndex - 1][sRowIndex, sColIndex];

                for (int i = 0; i < 4; i++)
                {
                    var bit = sRes.GetBit(i);
                    Helper.SetBit(ref result, subBlockIndex*subBlockResultSize + i, bit);
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

        private UInt32 Permutation(UInt32 block)
        {
            UInt32 resultBlock = 0;
            for (int i = 0; i < PTable.Length; i++)
            {
                var bit = block.GetBit(PTable[i] - 1);
                Helper.SetBit(ref resultBlock, i, bit);
            }
            return resultBlock;
        }

        #endregion Feistel Function

        #region IP Reverse

        private static readonly byte[] IpRTable =
        {
            40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25
        };

        private UInt64 InitialPermutationReverse(UInt64 block)
        {
            UInt64 resultBlock = 0;
            for (int i = 0; i < IpRTable.Length; i++)
            {
                var bit = block.GetBit(IpRTable[i] - 1);
                Helper.SetBit(ref resultBlock, i, bit);
            }
            return resultBlock;
        }

        #endregion IP Reverse

        #region Round Keys Generating

        private static readonly byte[] PC1Table =
        {
            57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4
        };

        private static readonly byte[] PC2Table =
        {
            14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4,
            26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40,
            51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32,
        };

        private static readonly byte[] KeyShiftTable =
        {
            1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1
        };

        private List<UInt64> GenerateRoundKeys(string key)
        {
            var intKey = UInt64.Parse(key);

            var roundKeys = new List<UInt64>(RoundsCount);

            UInt64 pKey = 0;
            for (int i = 0; i < PC1Table.Length; i++)
            {
                var bit = intKey.GetBit(PC1Table[i] - 1);
                Helper.SetBit(ref pKey, i, bit);
            }

            var sKey = pKey;
            for (int roundIndex = 0; roundIndex < RoundsCount; roundIndex++)
            {
                var leftHalfKey = (UInt32)(sKey >> 28);
                var rightHalfKey = (UInt32)((sKey << 36) >> 36);

                leftHalfKey = RotateLeft28Bits(leftHalfKey, KeyShiftTable[roundIndex]);
                rightHalfKey = RotateLeft28Bits(rightHalfKey, KeyShiftTable[roundIndex]);

                sKey = (UInt64)leftHalfKey << 28 | rightHalfKey;

                UInt64 roundKey = 0;
                for (int i = 0; i < PC2Table.Length; i++)
                {
                    var bit = sKey.GetBit(PC2Table[i] - 1);
                    Helper.SetBit(ref roundKey, i, bit);
                }
                roundKeys.Add(roundKey);
            }

            return roundKeys;
        }

        private UInt32 RotateLeft28Bits(UInt32 halfKey, byte n)
        {
            var result = (halfKey << n) | (halfKey >> (28 - n));
            for (int i = 0; i < n; i++)
            {
                Helper.SetBit(ref result, 28 + i, false);
            }
            return result;
        }

        #endregion Round Keys Generating


    }
}