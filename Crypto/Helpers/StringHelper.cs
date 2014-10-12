using System;
using System.Text;

namespace Crypto.Helpers
{
    public static class StringHelper
    {
        public static string ToCharString(this char[] array)
        {
            return new String(array);
        }

        public static int Mod(this int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        public static UInt32 AddWithMod2Pow32(this UInt32 x, UInt32 y)
        {
            UInt64 summ = x + y;
            UInt64 res = summ % UInt32.MaxValue;
            return (UInt32)res;
        }

        #region ToBitString

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

        public static string ToBitString(this UInt16 x)
        {
            char[] b = new char[16];
            int pos = 15;
            int i = 0;

            while (i < 16)
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

        public static string ToBitString(this Byte x)
        {
            char[] b = new char[8];
            int pos = 7;
            int i = 0;

            while (i < 8)
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

        #endregion ToBitString

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
