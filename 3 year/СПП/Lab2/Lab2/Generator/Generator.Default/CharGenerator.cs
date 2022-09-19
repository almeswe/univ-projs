using System;

namespace Generator.Default
{
    public sealed class CharGenerator : ValueGenerator
    {
        public CharGenerator() : base(typeof(char)) { }

        public override object Generate(Type type, GeneratorContext context) =>
            (char)this.RandRange('A', 'z');
    }
}
