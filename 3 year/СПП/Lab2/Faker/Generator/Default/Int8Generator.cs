using System;

namespace Generator.Default
{
    public sealed class Int8Generator : ValueGenerator
    {
        public Int8Generator() : base(typeof(sbyte)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (sbyte)_random.Next();
    }
}
