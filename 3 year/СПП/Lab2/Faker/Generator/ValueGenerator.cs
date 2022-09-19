using System;

namespace Generator
{
    public abstract class ValueGenerator
    {
        public Type Rule { get; protected set; }

        protected static Random _random = new Random(GenerateSeed());

        public ValueGenerator(Type rule) =>
            this.Rule = rule;

        public abstract object Generate(Type type, GeneratorContext context);

        public virtual bool CanGenerate(Type type) =>
            type == this.Rule;

        protected int RandRange(int min, int max) =>
            (int)(_random.Next() % (max + 1 - min) + min);

        protected static int GenerateSeed() =>
            DateTime.Now.Millisecond << 16 | DateTime.Now.Millisecond;
    }
}
