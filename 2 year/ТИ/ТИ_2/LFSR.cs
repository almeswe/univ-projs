namespace ТИ_2
{
    public static class LFSR
    {
        public static int Size => 40;
        public static byte[] Generate(long seed, long size)
        {
            byte[] stream = new byte[size];
            for (int @byte = 0; @byte < size; @byte++)
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
