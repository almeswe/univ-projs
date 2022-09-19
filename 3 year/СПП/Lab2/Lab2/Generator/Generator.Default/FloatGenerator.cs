using System;

namespace Generator.Default
{
    public sealed class FloatGenerator : ValueGenerator
    {
        public FloatGenerator() : base(typeof(float)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (float)(_random.Next() + _random.NextDouble());
    }
}
