using System;
using System.Text;

namespace Generator.Default
{
    public sealed class StringGenerator : ValueGenerator
    {
        public StringGenerator() : base(typeof(string)) { }
        
        public override object Generate(Type type, GeneratorContext context)
        {
            var builder = new StringBuilder();
            var charGenerator = new CharGenerator();
            var builderSize = (int)((byte)_random.Next());
            while (builder.Length != builderSize)
                builder.Append((char)charGenerator.Generate(typeof(char), context));
            return builder.ToString();
        }
    }
}
