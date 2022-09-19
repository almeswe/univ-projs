using System;

namespace Generator.Default
{
    public sealed class Int64Generator : ValueGenerator
    {
        public Int64Generator() : base(typeof(long)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            ((long)_random.Next() << 32) | (long)_random.Next();
    }
}
