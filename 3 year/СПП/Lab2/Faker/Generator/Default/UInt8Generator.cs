using System;

namespace Generator.Default
{
    public sealed class UInt8Generator : ValueGenerator
    {
        public UInt8Generator() : base(typeof(byte)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (byte)_random.Next();
    }
}
