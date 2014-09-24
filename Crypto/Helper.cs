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
