using System;

namespace Generator.Default
{
    public sealed class BoolGenerator : ValueGenerator
    {
        public BoolGenerator() : base(typeof(bool)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (_random.Next() & 1) == 1;
    }
}