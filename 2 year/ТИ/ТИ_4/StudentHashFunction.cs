using System.Numerics;

namespace ТИ_4
{
    public static class StudentHashFunction
    {
        public static BigInteger Hash(byte[] data, BigInteger n)
        {
            var result = new BigInteger(100);
            for (ulong i = 0; i < (ulong)data.Length; i++)
                result = BigInteger.Pow(result + data[i], 2) % n;
            return result;
        }
    }
}