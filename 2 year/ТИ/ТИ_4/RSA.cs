using System;
using System.Numerics;

namespace ТИ_4
{
    public static class RSA
    {
        public static bool IsPrime(BigInteger number)
        {
            if (number <= 1) 
                return false;
            if (number == 2) 
                return true;
            if (number % 2 == 0) 
                return false;
            var boundary = (int)Math.Floor(Math.Sqrt((double)number));
            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b) =>
            b == 0 ? a : GCD(b, a % b);

        public static BigInteger XGCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b < a)
            {
                var t = a;
                a = b;
                b = t;
            }

            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            var gcd = XGCD(b % a, a, out x, out y);
            var newY = x;
            var newX = y - (b / a) * x;
            x = newX;
            y = newY;
            return gcd;
        }

        public static BigInteger ModPow(BigInteger x, BigInteger y, BigInteger p)
        {
            BigInteger result = 1;
            for (BigInteger i = 1; i <= y; i++)
            {
                result *= x;
                result %= p;
            }
            return result;
        }

        public static BigInteger[] Encrypt(byte[] plainText, BigInteger e, BigInteger r)
        {
            var cipherText = new BigInteger[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
                cipherText[i] = BigInteger.ModPow(plainText[i], e, r);
            return cipherText; 
        }

        public static byte[] Decrypt(BigInteger[] cipherText, BigInteger d, BigInteger r)
        {
            var plainText = new byte[cipherText.Length];
            for (int i = 0; i < plainText.Length; i++)
                plainText[i] = (byte)BigInteger.ModPow(cipherText[i], d, r);
            return plainText;
        }
    }
}
