using System;

namespace Generator.Default
{
    public sealed class UInt64Generator : ValueGenerator
    {
        public UInt64Generator() : base(typeof(ulong)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (ulong)(((long)_random.Next() << 32) | (long)_random.Next());
    }
}
