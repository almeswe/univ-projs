using System;

namespace Generator.Default
{
    public sealed class Int16Generator : ValueGenerator
    {
        public Int16Generator() : base(typeof(short)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (short)_random.Next();
    }
}
