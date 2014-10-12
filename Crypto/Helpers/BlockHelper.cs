using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Helpers
{
    public static class BlockHelper
    {
        #region Split and Combine

        public static UInt64[] SplitInBlocks(byte[] message)
        {
            var blocksCount = message.Length / 8;
            if (blocksCount % 8 != 0)
                blocksCount++;
            var alignedMessage = new byte[blocksCount * 8];
            message.CopyTo(alignedMessage, 0);

            var blocks = new UInt64[blocksCount];
            for (int i = 0; i < blocksCount; i++)
            {
                blocks[i] = BitConverter.ToUInt64(alignedMessage, i * 8);
            }
            return blocks;
        }

        public static byte[] CombineBlocks(UInt64[] blocks)
        {
            var message = new byte[blocks.Length * 8];
            for (int i = 0; i < blocks.Length; i++)
            {
                BitConverter.GetBytes(blocks[i]).CopyTo(message, i * 8);
            }
            return message;
        }

        public static byte[] Trim(this byte[] arr, int length)
        {
            var trimmedArr = new byte[length];
            for (int i = 0; i < length; i++)
            {
                trimmedArr[i] = arr[i];
            }
            return trimmedArr;
        }

        #endregion Split and Combine

        #region Permutate

        public static UInt64 PermutateBlock(UInt64 block, byte[] table,
            bool startFrom0 = true)
        {
            UInt64 resultBlock = 0;
            for (int i = 0; i < table.Length; i++)
            {
                var bit = block.GetBit(startFrom0 ? table[i] : table[i] - 1);
                BitHelper.SetBit(ref resultBlock, i, bit);
            }
            return resultBlock;
        }

        #endregion Permutate
    }
}
