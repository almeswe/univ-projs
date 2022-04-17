using System;

namespace ТИ_2
{
    public static class LFSR
    {
        public static int Size => 40;
        public static byte[] Generate(long seed, long size)
        {
            byte[] stream = new byte[size];
            for (int i = 0; i < Math.Min(size, 5); i++)
                stream[i] = (byte)(seed >> (32 - (i * 8)));

            for (int @byte = 5; @byte < size; @byte++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    long xored = ((seed >> 39) ^ (seed >> 20) ^
                        (seed >> 18) ^ (seed >> 1)) & 1;
                    seed = ((seed << 1) | xored);
                    stream[@byte] = (byte)((byte)(
                        stream[@byte] << 1) | xored);
                }
            }
            return stream;
        }
    }
}
