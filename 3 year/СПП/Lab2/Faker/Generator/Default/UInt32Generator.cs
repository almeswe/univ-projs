using System;

namespace Generator.Default
{
    public sealed class UInt32Generator : ValueGenerator
    {
        public UInt32Generator() : base(typeof(uint)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (uint)_random.Next();
    }
}
