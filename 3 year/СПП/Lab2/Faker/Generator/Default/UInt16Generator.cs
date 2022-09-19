using System;

namespace Generator.Default
{
    public sealed class UInt16Generator : ValueGenerator
    {
        public UInt16Generator() : base(typeof(ushort)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (ushort)_random.Next();
    }
}
