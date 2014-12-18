using System.Collections.Concurrent;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto.Assymetric
{
    public class PrimeNumberGenerator
    {
        private readonly RandomNumberGenerator _rng;
        
        public PrimeNumberGenerator()
        {
            _rng = RandomNumberGenerator.Create();
        }
        
        private void GenerateSandD(BigInteger n, out int s, out BigInteger d)
        {
            s = 0;
            d = n - BigInteger.One;
            while (d.IsEven)
            {
                d /= 2;
                s++;
            }
        }

        private bool Pass(BigInteger n, BigInteger a, int s, BigInteger d)
        {
            BigInteger nMinusOne = n - BigInteger.One;
            BigInteger aToPower = BigInteger.ModPow(a, d, n);
            if (aToPower == BigInteger.One) return true;
            for (int i = 0; i < s - 1; i++)
            {
                if (aToPower == nMinusOne) return true;
                aToPower = BigInteger.ModPow(aToPower, 2, n);
            }
            if (aToPower == nMinusOne) return true;
            return false;
        }

        private readonly int[] _testPrimes = {2, 3, 5, 7, 11, 13, 17, 19, 23};

        private bool CheckSimplePrimes(BigInteger n)
        {
            return _testPrimes.All(p => !BigInteger.Remainder(n, p).IsZero);
        }

        public BigInteger NextBigInteger(int bitLength)
        {
            BigInteger res;
            do
            {
                var bytes = new byte[bitLength / 8];
                _rng.GetBytes(bytes);
                res = new BigInteger(bytes);
            } while (res.IsZero || res < 0);
            return res;
        }

        public bool CheckPrime(BigInteger n, int bitLength = -1)
        {
            if (bitLength == -1) bitLength = n.ToByteArray().Length;
            if (!CheckSimplePrimes(n)) return false;

            int s;
            BigInteger d;
            GenerateSandD(n, out s, out d);
            for (int i = 0; i < 200; i++)
            {
                var a = NextBigInteger(bitLength);
                if (!Pass(n, a, s, d)) return false;
            }
            return true;
        }

        private CancellationTokenSource _cts;

        public BigInteger NextPrimeBigInteger(int bitLength = 512)
        {
            _cts = new CancellationTokenSource();
            
            var tasks = new Task<BigInteger>[4];
            for (int i = 0; i < 4; i++)
            {
                
                tasks[i] = Task.Factory.StartNew(() => NextPrimeBigInt(bitLength, _cts.Token));
                tasks[i].ContinueWith(t => _cts.Cancel());
            }

// ReSharper disable once CoVariantArrayConversion
            Task.WaitAny(tasks);

            return tasks.First(t => t.Result != 0).Result;

        }

        private BigInteger NextPrimeBigInt(int bitLength, CancellationToken ct)
        {
            BigInteger res = BigInteger.Zero;
            do
            {
                if (ct.IsCancellationRequested) break;
                res = NextBigInteger(bitLength);
            } while (!CheckPrime(res, bitLength));
            return res;
        }
    }

    #region TEST
    public static class BigIntegerExtensions
    {
        public static bool IsProbablePrime(this BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
    #endregion TEST
}
