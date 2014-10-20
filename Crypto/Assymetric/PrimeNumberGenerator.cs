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

        public BigInteger NextBigInteger(int bitLength = 1024)
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
            for (int i = 0; i < 20; i++)
            {
                var a = NextBigInteger(bitLength);
                if (!Pass(n, a, s, d)) return false;
            }
            return true;
        }

        private CancellationTokenSource _cts;

        public BigInteger NextPrimeBigInteger(int bitLength = 1024)
        {
            _cts = new CancellationTokenSource();
            
            var tasks = new Task<BigInteger>[4];
            for (int i = 0; i < 4; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => NextPrimeBigInt(bitLength), _cts.Token);
                tasks[i].ContinueWith(t => _cts.Cancel());
            }

// ReSharper disable once CoVariantArrayConversion
            var tIndex = Task.WaitAny(tasks, _cts.Token);

            return tasks[tIndex].Result;

        }

        private BigInteger NextPrimeBigInt(int bitLength = 1024)
        {
            BigInteger res = BigInteger.Zero;
            do
            {
                if (_cts.IsCancellationRequested) break;
                res = NextBigInteger();
            } while (!CheckPrime(res, bitLength));
            return res;
        }
    }
}
