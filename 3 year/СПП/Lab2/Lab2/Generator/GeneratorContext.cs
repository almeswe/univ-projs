using Faker;

namespace Generator
{
    public sealed class GeneratorContext
    {
        public IFaker Faker { get; }
        public FakerRules Rules { get; }

        public GeneratorContext(IFaker faker, FakerRules rules)
        {
            this.Faker = faker;
            this.Rules = rules;
        }
    }
}