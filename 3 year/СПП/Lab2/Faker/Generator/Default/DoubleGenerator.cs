using System;

namespace Generator.Default
{
    public sealed class DoubleGenerator : ValueGenerator
    {
        public DoubleGenerator() : base(typeof(double)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (double)(_random.Next() + _random.NextDouble());
    }
}
