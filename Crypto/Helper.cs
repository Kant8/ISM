using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public static class Helper
    {
        public static string ToCharString(this char[] array)
        {
            return new String(array);
        }

        public static int Mod(this int x, int m)
        {
            int r = x % m;
            return r > 0 ? r + m : r;
        }

        public static UInt32 RotateLeft(this UInt32 x, byte n)
        {
            return (x << n) | (x >> (32 - n));
        }

        public static UInt32 RotateRight(this UInt32 x, byte n)
        {
            return (x >> n) | (x << (32 - n));
        }

        public static UInt32 HiWord(this UInt64 x)
        {
            return (UInt32)(x >> 32);
        }

        public static UInt32 LoWord(this UInt64 x)
        {
            return (UInt32)(x & UInt32.MaxValue);
        }

        public static UInt64 Combine(this UInt32 hiWord, UInt32 loWord)
        {
            return (UInt64)hiWord << 32 | loWord;
        }

        public static string ToBitString(this UInt64 x)
        {
            char[] b = new char[64];
            int pos = 63;
            int i = 0;

            while (i < 64)
            {
                if ((x & ((UInt64)1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }

        public static string ToBitString(this UInt32 x)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((x & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }

        public static void SetBit(ref UInt64 x, int index, bool bit)
        {
            UInt64 mask = (UInt64)1 << index;
            if (bit)
                x &= ~mask;
            else
                x |= mask;
        }

        public static void SetBit(ref UInt32 x, int index, bool bit)
        {
            UInt32 mask = (UInt32)1 << index;
            if (bit)
                x &= ~mask;
            else
                x |= mask;
        }

        public static void SetBit(ref Byte x, int index, bool bit)
        {
            Byte mask = (Byte)(1 << index);
            if (bit)
                x &= (Byte)~mask;
            else
                x |= mask;
        }

        public static bool GetBit(this UInt64 x, int index)
        {
            UInt64 mask = (UInt64)1 << index;
            var res = x & mask;
            return res > 0;
        }

        public static bool GetBit(this UInt32 x, int index)
        {
            UInt32 mask = (UInt32)1 << index;
            var res = x & mask;
            return res > 0;
        }

        public static bool GetBit(this Byte x, int index)
        {
            Byte mask = (Byte)(1 << index);
            var res = x & mask;
            return res > 0;
        }

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

        public static string GetUtf16String(this byte[] arr)
        {
            return Encoding.Unicode.GetString(arr);
        }

        public static byte[] GetUtf16Bytes(this string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }
    }
}
