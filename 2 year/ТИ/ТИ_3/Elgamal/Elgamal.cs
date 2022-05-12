using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace ТИ_3.Elgamal
{
    public static class Elgamal
    {
        public static ulong Euler(ulong number)
        {
            ulong result = 1;
            if (number < 1)
                throw new ArgumentException("Number must be greater or equal to 1.");
            for (ulong i = 2; i < number; i++)
                if (GetCommonDivisor(i, number) == 1)
                    result++;
            return result;
        }

        public static ulong FastExp(ulong x, ulong y, ulong p)
        {
            ulong res = 1;
            x = x % p;
            if (x == 0)
                return 0;
            while (y > 0)
            {
                if ((y & 1) != 0)
                    res = (res * x) % p;
                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }

        public static bool IsPrime(ulong number)
        {
            if (number == 2)
                return true;
            if (number < 2 || number % 2 == 0)
                return false;
            for (ulong i = 3; i <= Math.Sqrt(number); i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }

        public static List<ulong> GetPrimeRoots(ulong p)
        {
            var roots = new List<ulong>();
            for (ulong g = 1; g <= p-1; g++)
            {
                var isNotRoot = true;
                if (FastExp(g, p-1, p) == 1)
                {
                    for (ulong l = 1; l <= p-2; l++)
                    {
                        if (isNotRoot = (FastExp(g, l, p) == 1))
                            break;
                    }
                    if (!isNotRoot)
                        roots.Add(g);
                }
            }
            return roots;
            #region Garbage
            //if (p < 2 || !IsPrime(p))
            //    throw new ArgumentException("P is invalid.");
            //var roots = new List<ulong>();
            //var divisors = GetPrimeDivisors(p - 1)
            //    .Where(d => d != 1 && d != (p - 1));
            //for (int g = 2; g < p; g++)
            //{
            //    bool skip = false;
            //    foreach (var divisor in divisors)
            //        if (skip = (Math.Pow(g, (p - 1) / divisor) % p == 1))
            //            break;
            //    if (!skip)
            //        roots.Add(g);
            //}
            //return roots.ToList();
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

        public static List<ulong> GetDivisors(ulong number)
        {
            var factors = new List<ulong>();
            var max = (ulong)Math.Sqrt(number);

            for (ulong factor = 1; factor <= max; ++factor)
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

        public static ulong GetCommonDivisor(ulong a, ulong b) =>
            a == 0 ? b : GetCommonDivisor(b % a, a);

        public static List<ulong> GetPrimeDivisors(ulong number) =>
            GetDivisors(number).Where(d => IsPrime(d)).ToList();
    
        public static List<ulong> GeneratePublicKey(ulong p, ulong g, ulong x) =>
            new List<ulong>() { p, g, FastExp(g, x, p) };

        public static ulong[] Encrypt(byte[] plainText, ulong p, ulong g, ulong k, ulong y)
        {
            var cipherText = new ulong[plainText.Length*2];
            var a = FastExp(g, k, p);
            for (int i = 0; i < cipherText.Length; i+=2)
            {
                cipherText[i] = a;
                cipherText[i+1] = (ulong)(BigInteger.Pow(y, (int)k) * plainText[i/2] % p);
            }
            return cipherText;
        }

        public static byte[] Decrypt(ulong[] cipherText, ulong p, ulong x)
        {
            var plainText = new byte[cipherText.Length / 2];
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                plainText[i / 2] = (byte)(BigInteger.Pow(
                    cipherText[i], (int)(x * (p - 2))) * cipherText[i + 1] % p);
            }
            return plainText;
        }
    }
}