using System;

namespace Crypto.Helpers
{
    public static class BitHelper
    {
        #region Rotate

        public static UInt32 RotateLeft(this UInt32 x, byte n)
        {
            return (x << n) | (x >> (32 - n));
        }

        public static UInt32 RotateRight(this UInt32 x, byte n)
        {
            return (x >> n) | (x << (32 - n));
        }

        #endregion Rotate

        #region SetBit

        public static void SetBit(ref UInt64 x, int index, bool bit)
        {
            UInt64 mask = (UInt64)1 << index;
            if (bit)
                x |= mask;
            else
                x &= ~mask;
        }

        public static void SetBit(ref UInt32 x, int index, bool bit)
        {
            UInt32 mask = (UInt32)1 << index;
            if (bit)
                x |= mask;
            else
                x &= ~mask;
        }

        public static void SetBit(ref UInt16 x, int index, bool bit)
        {
            UInt16 mask = (UInt16)(1 << index);
            if (bit)
                x |= mask;
            else
                x &= (UInt16)~mask;
        }

        public static void SetBit(ref Byte x, int index, bool bit)
        {
            Byte mask = (Byte)(1 << index);
            if (bit)
                x |= mask;
            else
                x &= (Byte)~mask;
        }

        #endregion SetBit

        #region GetBit

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

        public static bool GetBit(this UInt16 x, int index)
        {
            UInt16 mask = (UInt16)(1 << index);
            var res = x & mask;
            return res > 0;
        }

        public static bool GetBit(this Byte x, int index)
        {
            Byte mask = (Byte)(1 << index);
            var res = x & mask;
            return res > 0;
        }

        #endregion GetBit

        #region Split and Combine

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

        #endregion Split and Combine

    }
}
