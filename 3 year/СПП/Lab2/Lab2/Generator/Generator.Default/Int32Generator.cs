using System;

namespace Generator.Default
{
    public sealed class Int32Generator : ValueGenerator
    {
        public Int32Generator() : base(typeof(int)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            _random.Next();
    }
}
