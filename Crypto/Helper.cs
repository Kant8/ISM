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

        static string ToBitString(this UInt32 x)
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
