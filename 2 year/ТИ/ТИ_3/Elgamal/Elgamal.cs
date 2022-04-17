using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТИ_3.Elgamal
{
    public static class Elgamal
    {
        public static long Euler(long number)
        {
            int result = 1;
            if (number < 1)
                throw new ArgumentException("Number must be greater or equal to 1.");
            for (int i = 2; i < number; i++)
                if (GetCommonDivisor(i, number) == 1)
                    result++;
            return result;
        }

        public static bool IsPrime(long number)
        {
            if (number == 2)
                return true;
            if (number < 2 || number % 2 == 0)
                return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }

        public static List<long> GetPrimeRoots(long p)
        {
            if (p < 2 || !IsPrime(p))
                throw new ArgumentException("P is invalid.");
            var roots = new List<long>();
            var divisors = GetPrimeDivisors(p - 1)
                .Where(d => d != 1 && d != (p - 1));
            for (int g = 2; g < p; g++)
            {
                bool skip = false;
                foreach (var divisor in divisors)
                    if (skip = (Math.Pow(g, (p - 1) / divisor) % p == 1))
                        break;
                if (!skip)
                    roots.Add(g);
            }
            return roots.ToList();
            #region Garbage
            //if (p < 2 || !IsPrime(p))
            //    throw new ArgumentException("P is invalid.");
            //var count = Euler(Euler(p));
            //var roots = new List<int>();
            //var divisors = GetPrimaryDivisors(p - 1).Where(d => d != 1 && d != (p-1));
            //while (roots.Count != count)
            //{
            //    var skip = false;
            //    var g = _random.Next(2, p);
            //        foreach (var divisor in divisors)
            //            if (skip = (Math.Pow(g, divisor) % p == 1))
            //                break;
            //    if (!skip && !roots.Contains(g))
            //        roots.Add(g);
            //}
            //roots.Sort();
            //return roots.Distinct().ToList();
            //if (p < 2 || !IsPrime(p))
            //    throw new ArgumentException("P is invalid.");
            //var roots = new List<int>();
            //var divisors = GetPrimaryDivisors(p - 1);
            //for (int g = 2; g < p; g++)
            //{
            //    var skip = false;
            //    foreach (var divisor in divisors)
            //        if (skip = (Math.Pow(g, (p-1)/divisor) % p == 1))
            //            break;
            //    if (!skip)
            //        roots.Add(g);
            //}
            //roots.Sort();
            //return roots.Distinct().ToList();
            //if (p < 2 || !IsPrime(p))
            //    throw new ArgumentException("P is invalid.");
            //var roots = new List<double>(Euler(p - 1));
            //var divisors = GetDivisors(p - 1)
            //    .Where(d => d != 1 && d != (p - 1));
            //for (int g = 2; g < p; g++)
            //{
            //    bool skip = false;
            //    foreach (var divisor in divisors)
            //        if (skip = ((Math.Pow(g, divisor)-1) % p == 0))
            //            if ((Math.Pow(g, divisor) - 1) % p == 1)
            //                break;
            //    if (!skip)
            //        roots.Add(g);
            //}
            //return roots.ToList();
            #endregion
        }

        public static List<long> GetDivisors(long number)
        {
            var factors = new List<long>();
            var max = (long)Math.Sqrt(number);

            for (int factor = 1; factor <= max; ++factor)
            {
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor)
                        factors.Add(number / factor);
                }
            }
            return factors;
        }

        public static long GetCommonDivisor(long a, long b) =>
            a == 0 ? b : GetCommonDivisor(b % a, a);

        public static List<long> GetPrimeDivisors(long number) =>
            GetDivisors(number).Where(d => IsPrime(d)).ToList();
    
        public static List<long> GeneratePublicKey(long p, long g, long x) =>
            new List<long>() { p, g, (long)Math.Pow(g, x) % p };

        public static long[] Encrypt(byte[] plainText, long p, long g, long k, long y)
        {
            var cipherText = new long[plainText.Length*2];
            for (int i = 0; i < cipherText.Length; i+=2)
            {
                cipherText[i] = (long)Math.Pow(g, k) % p;
                cipherText[i+1] = ((long)Math.Pow(y, k) * plainText[i/2]) % p;
            }
            return cipherText;
        }

        public static byte[] Decrypt(long[] cipherText, long p, long x)
        {
            var plainText = new byte[cipherText.Length / 2];
            for (int i = 0; i < cipherText.Length; i += 2)
                plainText[i/2] = (byte)(cipherText[i+1] * Math.Pow(Math.Pow(cipherText[i], x), p-2) % p);
            return plainText;
        }
    }
}